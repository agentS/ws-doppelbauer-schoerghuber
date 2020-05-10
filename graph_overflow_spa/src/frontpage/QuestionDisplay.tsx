import React from "react";
import { Accordion, Button, Card } from "react-bootstrap";

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
                        <h3>{this.props.question.title}</h3>
                    </Accordion.Toggle>
                </Card.Header>
                <Accordion.Collapse eventKey={this.props.question.id}>
                    <Card.Body>{this.props.question.content}</Card.Body>
                </Accordion.Collapse>
            </Card>
        );
    }
}

export default QuestionDisplay;
