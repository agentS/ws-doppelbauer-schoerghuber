import React from "react";
import { Row, Col } from "react-bootstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";

import moment from "moment";
import { MOMENT_DATE_TIME_FORMAT_STRING } from "../Constants";

import { FetchQuestionComponent } from "../graphql/GraphQlTypes";

interface QuestionDisplayProperties extends RouteComponentProps {
    questionId: number;
}

interface QuestionDisplayState {}

function formatDateTime(dateTime: string): string {
    return moment(dateTime).format(MOMENT_DATE_TIME_FORMAT_STRING);
}

class QuestionDisplay extends React.Component<QuestionDisplayProperties, QuestionDisplayState> {
    render() {
        return (
            <FetchQuestionComponent variables={{ questionId: this.props.questionId }}>
                {({ loading, error, data }) => {
                    if (loading) {
                        return (<h3>Loading question</h3>);
                    }

                    if (error || !data) {
                        return (<h3>could not load question</h3>);
                    }

                    return (
                        <div>
                            <Row>
                                <Col xs={2}>{data.question.upVotes} upvote(s)</Col>
                                <Col>
                                    <h4>{data.question.title}</h4>
                                    <p>{data.question.content}</p>
                                    <p>Created by {data.question.user.name} at {formatDateTime(data.question.createdAt)}</p>
                                    <h6>
                                        Tags: {data.question.tags.map(tag => (
                                            <span key={tag.id}>{tag.name} </span>
                                        ))}
                                    </h6>
                                </Col>
                            </Row>
                            <hr />
                        </div>
                    );
                }}
            </FetchQuestionComponent>
        );
    }
}

export default withRouter(QuestionDisplay);
