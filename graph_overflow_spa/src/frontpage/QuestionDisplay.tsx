import React from "react";

import { Question } from "../graphql/GraphQlSchemaTypes";

interface QuestionDisplayProperties
{
    question: Question
};

interface QuestionDisplayState {}

class QuestionDisplay extends React.Component<QuestionDisplayProperties, QuestionDisplayState> {
    render() {
        return (
            <div>
                <h3>{this.props.question.title}</h3>
                <p>{this.props.question.content}</p>
            </div>
        );
    }
}

export default QuestionDisplay;
