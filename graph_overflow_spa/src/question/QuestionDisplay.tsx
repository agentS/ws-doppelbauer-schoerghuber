import React from "react";
import { Row, Col, Badge } from "react-bootstrap";

import { Question, Answer } from "../graphql/GraphQlTypes";
import { formatDateTime } from "../DateTimeUtilities";
import { isLoggedIn } from "../authentication/AuthenticationUtils";

import AnswerDisplay from "./AnswerDisplay";
import PostAnswer from "./PostAnswer";
import UpVoteButton, { UpVoteButtonMode, UpVoteCallback } from "./UpVoteButton";

interface QuestionDisplayProperties {
    question: Question;
    onUpVoteQuestion: UpVoteCallback;
    onUpVoteAnswer: UpVoteCallback;
}

interface QuestionDisplayState {}

class QuestionDisplay extends React.Component<QuestionDisplayProperties, QuestionDisplayState> {

    render() {
        return (
            <div>
                <Row>
                    <Col xs={2}>
                        <p>{this.props.question.upVotes} upvote(s)</p>
                        <UpVoteButton
                            mode={UpVoteButtonMode.QUESTION}
                            postId={parseInt(this.props.question.id)}
                            upVoteUsers={this.props.question.upVoteUsers}
                            onUpvote={(questionId, newUpVoteCount, newUpVoteUsers) => this.props.onUpVoteQuestion(questionId, newUpVoteCount, newUpVoteUsers)}
                        />
                    </Col>
                    <Col>
                        <h4>{this.props.question.title}</h4>
                        <p>{this.props.question.content}</p>
                        <p>{this.props.question.tags.map(tag => <span><Badge variant="dark">{tag.name}</Badge>{' '}</span>)}</p>
                        <p>Created by {this.props.question.user.name} at {formatDateTime(this.props.question.createdAt)}</p>
                    </Col>
                </Row>
                <hr />

                {this.props.question.answers.map(answer => (
                    <div key={answer.id}>
                        <AnswerDisplay
                            answer={answer as Answer}
                            onUpVoteAnswer={(answerId, newUpVoteCount, newUpVoteUsers) => this.props.onUpVoteAnswer(answerId, newUpVoteCount, newUpVoteUsers)}
                        />
                        <hr />
                    </div>
                ))}

                {
                    isLoggedIn()
                    ? (<PostAnswer questionId={parseInt(this.props.question.id, 10)} />)
                    : (<span></span>)
                }
            </div>
        );
    }
}

export default QuestionDisplay;
