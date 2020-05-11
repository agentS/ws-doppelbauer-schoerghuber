import React from "react";
import { Accordion, Button, Card, Col, Row } from "react-bootstrap";

import { Question } from "../graphql/GraphQlSchemaTypes";

interface QuestionDisplayProperties
{
    question: Question
};

interface QuestionDisplayState {}

class QuestionDisplay extends React.Component<QuestionDisplayProperties, QuestionDisplayState> {
    render() {
        return (
            <Card>
                <Card.Header>
                    <Accordion.Toggle as={Button} variant="link" eventKey={this.props.question.id}>
                        <Row>
                            <Col xs={6}>{this.props.question.upVotes} Upvote(s)</Col>
                            <Col>{this.props.question.title}</Col>
                        </Row>
                    </Accordion.Toggle>
                </Card.Header>
                <Accordion.Collapse eventKey={this.props.question.id}>
                    <Card.Body>
                        {this.props.question.content}
                    </Card.Body>
                </Accordion.Collapse>
            </Card>
        );
    }
}

export default QuestionDisplay;
