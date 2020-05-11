import React from "react";
import { Row, Col } from "react-bootstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";
import update from "immutability-helper";

import {
    FetchQuestionComponent,
    AnswerAddedComponent, CommentAddedComponent,
    User, Question, Answer, Comment
} from "../graphql/GraphQlTypes";
import { formatDateTime } from "../DateTimeUtilities";
import { isLoggedIn } from "../authentication/AuthenticationUtils";

import AnswerDisplay from "./AnswerDisplay";
import PostAnswer from "./PostAnswer";
import UpVoteButton, { UpVoteButtonMode } from "./UpVoteButton";

interface QuestionDisplayProperties extends RouteComponentProps {
    questionId: number;
}

interface QuestionDisplayState {
    question: (Question | null);
}

class QuestionDisplay extends React.Component<QuestionDisplayProperties, QuestionDisplayState> {
    constructor(properties: QuestionDisplayProperties) {
        super(properties);

        this.state = {
            question: null
        };
    }

    onUpVoteQuestion(questionId: number, newUpVoteCount: number, newUpVoteUsers: User[]) {
        if (this.state.question) {
            this.setState({
                question: update(
                    this.state.question,
                    {
                        upVotes: { $set: newUpVoteCount },
                        upVoteUsers: { $set: newUpVoteUsers }
                    }
                )
            });
        }
    }

    onUpVoteAnswer(answerId: number, newUpVoteCount: number, newUpVoteUsers: User[]) {
        if (this.state.question) {
            const modifiedAnswerIndex = this.state.question.answers
                .findIndex(answer => parseInt(answer.id) === answerId);
            if (modifiedAnswerIndex && modifiedAnswerIndex !== (-1)) {
                const newAnswer = update(
                    this.state.question.answers[modifiedAnswerIndex],
                    {
                        upVotes: { $set: newUpVoteCount },
                        upVoteUsers: { $set: newUpVoteUsers }
                    }
                );
                this.setState({
                    question: update(
                        this.state.question,
                        {
                            answers: { $splice: [[ modifiedAnswerIndex, 1, newAnswer ]] } 
                        }
                    )
                });
            }
        }
    }

    onNewAnswer(newAnswer: Answer) {
        if (
            (this.state.question)
            && (!this.state.question.answers.find(answer => answer.id === newAnswer.id))
        ) {
            this.setState({
                question: update(
                    this.state.question,
                    {
                        answers: { $push: [ newAnswer ] }
                    }
                )
            });
        }
    }

    onNewComment(newComment: Comment) {
        if (this.state.question) {
            const answer = this.state.question.answers.find(answer => answer.id === newComment.answer.id);
            if (
                (answer)
                && (!answer.comments.find(comment => comment.id === newComment.id))
            ) {
                const nextAnswer = update(
                    answer,
                    {
                        comments: { $push: [ newComment ] }
                    }
                );
                const answerIndex = this.state.question.answers.indexOf(answer);
                this.setState({
                    question: update(
                        this.state.question,
                        {
                            answers: { $splice: [[ answerIndex, 1, nextAnswer ]] }
                        }
                    )
                });
            }
        }
    }

    render() {
        if (this.state.question === null) {
            return (
                <FetchQuestionComponent variables={{ questionId: this.props.questionId }}
                    onCompleted={(result) =>  this.setState({ question: result.question as Question })}
                >
                    {({ loading, error, data }) => {
                        if (loading) {
                            return (<h3>Loading question</h3>);
                        }

                        if (error || !data) {
                            return (<h3>could not load question</h3>);
                        }

                        return (<h3>Loading finished</h3>);
                    }}
                </FetchQuestionComponent>
            );
        } else {
            return (
                <div>
                    <AnswerAddedComponent
                        variables={{ questionId: parseInt(this.state.question.id) }}
                        shouldResubscribe={true}
                        onSubscriptionComplete={() => console.log("Subscribing for latest answers")}
                    >
                        {({ loading, error, data}) => {
                            if (loading) {
                                console.log("Started subscribing for latest answers");
                            }
                            if (error) {
                                alert("Could not subscribe to the latest answers!");
                                console.error(error);
                            }

                            if (data && data.answerAdded) {
                                this.onNewAnswer(data.answerAdded as Answer);
                            }
                            return (<span></span>);
                        }}
                    </AnswerAddedComponent>
                    <CommentAddedComponent
                        variables={{ questionId: parseInt(this.state.question.id) }}
                        shouldResubscribe={true}
                        onSubscriptionComplete={() => console.log("Subscribing for latest comments")}
                    >
                        {({ loading, error, data }) => {
                            if (loading) {
                                console.log("Started subscribing for latest comments");
                            }
                            if (error) {
                                alert("Could not subscribe to the latest comments!");
                                console.error(error);
                            }

                            if (data && data.commentAddedToAnswerOfQuestion) {
                                this.onNewComment(data.commentAddedToAnswerOfQuestion as Comment);
                            }
                            return (<span></span>);
                        }}
                    </CommentAddedComponent>
                    <Row>
                        <Col xs={2}>
                            <p>{this.state.question.upVotes} upvote(s)</p>
                            <UpVoteButton
                                mode={UpVoteButtonMode.QUESTION}
                                postId={parseInt(this.state.question.id)}
                                upVoteUsers={this.state.question.upVoteUsers}
                                onUpvote={(questionId, newUpVoteCount, newUpVoteUsers) => this.onUpVoteQuestion(questionId, newUpVoteCount, newUpVoteUsers)}
                            />
                        </Col>
                        <Col>
                            <h4>{this.state.question.title}</h4>
                            <p>{this.state.question.content}</p>
                            <p>Created by {this.state.question.user.name} at {formatDateTime(this.state.question.createdAt)}</p>
                        </Col>
                    </Row>
                    <hr />

                    {this.state.question.answers.map(answer => (
                        <div key={answer.id}>
                            <AnswerDisplay
                                answer={answer as Answer}
                                onUpVoteAnswer={(answerId, newUpVoteCount, newUpVoteUsers) => this.onUpVoteAnswer(answerId, newUpVoteCount, newUpVoteUsers)}
                            />
                            <hr />
                        </div>
                    ))}

                    {
                        isLoggedIn()
                        ? (<PostAnswer questionId={parseInt(this.state.question.id, 10)} />)
                        : (<span></span>)
                    }
                </div>
            );
        }
    }
}

export default withRouter(QuestionDisplay);
