import React from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";
import update from "immutability-helper";

import {
    FetchQuestionComponent,
    AnswerAddedComponent, CommentAddedComponent,
    User, Question, Answer, Comment
} from "../graphql/GraphQlTypes";

import QuestionDisplay from "./QuestionDisplay";

interface QuestionPageProperties extends RouteComponentProps {
    questionId: number;
}

interface QuestionPageState {
    question: (Question | null);
}

class QuestionPage extends React.Component<QuestionPageProperties, QuestionPageState> {
    constructor(properties: QuestionPageProperties) {
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
                    <QuestionDisplay question={this.state.question}
                        onUpVoteQuestion={(answerId, newUpVoteCount, newUpVoteUsers) => this.onUpVoteQuestion(answerId, newUpVoteCount, newUpVoteUsers)}
                        onUpVoteAnswer={(answerId, newUpVoteCount, newUpVoteUsers) => this.onUpVoteAnswer(answerId, newUpVoteCount, newUpVoteUsers)}
                    />
                </div>
            );
        }
    }
}

export default withRouter(QuestionPage);
