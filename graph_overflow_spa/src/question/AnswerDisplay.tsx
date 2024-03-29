import React from "react";
import { Row, Col, Accordion, Card, Button } from "react-bootstrap";

import { Answer } from "../graphql/GraphQlTypes";
import { formatDateTime } from "../DateTimeUtilities";
import { isLoggedIn } from "../authentication/AuthenticationUtils";

import CommentDisplay from "./CommentDisplay";
import PostComment from "./PostComment";
import UpVoteButton, { UpVoteButtonMode, UpVoteCallback } from "./UpVoteButton";

interface AnswerDisplayProperties {
    answer: Answer;
    onUpVoteAnswer: UpVoteCallback;
}
interface AnswerDisplayState {}

class AnswerDisplay extends React.Component<AnswerDisplayProperties, AnswerDisplayState> {

    render() {
        return (
            <div>
                <Row>
                    <Col xs={2}>
                        <p>{this.props.answer.upVotes} upvote(s)</p>
                        <UpVoteButton
                            mode={UpVoteButtonMode.ANSWER}
                            postId={parseInt(this.props.answer.id)}
                            upVoteUsers={this.props.answer.upVoteUsers}
                            onUpvote={(answerId, newUpVoteCount, newUpVoteUsers) => this.props.onUpVoteAnswer(answerId, newUpVoteCount, newUpVoteUsers)}
                        />
                    </Col>
                    <Col>
                        <p>{this.props.answer.content}</p>
                        <p>Answered by {this.props.answer.user.name} at {formatDateTime(this.props.answer.createdAt)}</p>
                    </Col>
                </Row>
                <Accordion>
                    <Card>
                        <Card.Header>
                            <Accordion.Toggle as={Button} variant="link" eventKey={this.props.answer.id}>
                                Show comments
                            </Accordion.Toggle>
                        </Card.Header>
                        <Accordion.Collapse eventKey={this.props.answer.id}>
                            <Card.Body>
                                {this.props.answer.comments.map(comment => (
                                    <CommentDisplay key={comment.id} comment={comment} />
                                ))}
                                {
                                    isLoggedIn()
                                    ? (<PostComment answerId={parseInt(this.props.answer.id)} />)
                                    : (<span></span>)
                                }
                            </Card.Body>
                        </Accordion.Collapse>
                    </Card>
                </Accordion>
            </div>
        );
    }
}

export default AnswerDisplay;
