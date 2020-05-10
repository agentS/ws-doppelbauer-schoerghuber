import React from "react";
import { Row, Col } from "react-bootstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { FetchQuestionComponent, Question, Answer } from "../graphql/GraphQlTypes";
import { formatDateTime } from "../DateTimeUtilities";
import { isLoggedIn } from "../authentication/AuthenticationUtils";

import AnswerDisplay from "./AnswerDisplay";
import PostAnswer from "./PostAnswer";
import UpVoteButton from "./UpVoteButton";

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
                    <Row>
                        <Col xs={2}>
                            <p>{this.state.question.upVotes} upvote(s)</p>
                            <div>
                                <UpVoteButton
                                    questionId={parseInt(this.state.question.id)}
                                    upVotes={this.state.question.upVotes}
                                />
                            </div>
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
                            <AnswerDisplay answer={answer as Answer} />
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
