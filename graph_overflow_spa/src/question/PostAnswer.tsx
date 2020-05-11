import React from "react";
import { Form, Button } from "react-bootstrap";

import { AnswerQuestionComponent } from "../graphql/GraphQlTypes";

interface PostAnswerProperties {
    questionId: number;
}
interface PostAnswerState {
    answer: string;
}

class PostAnswer extends React.Component<PostAnswerProperties, PostAnswerState> {
    constructor(properties: PostAnswerProperties) {
        super(properties);

        this.state = {
            answer: ""
        };
    }

    setAnswer(answer: string) {
        this.setState({ answer });
    }

    render() {
        return (
            <div>
                <h3>Post your answer</h3>
                <AnswerQuestionComponent>
                    {mutate => (
                        <Form onSubmit={(event: React.FormEvent<HTMLFormElement>) => {
                            event.preventDefault();
                            
                            mutate({ variables: {
                                questionId: this.props.questionId,
                                content: this.state.answer,
                            }})
                                .then(result => {
                                    alert("Answer posted!");
                                })
                                .catch(exception => {
                                    alert("Could not post the answer.");
                                    console.error(exception);
                                });
                        }}>
                            <Form.Group controlId="answer">
                                <Form.Label>Your answer</Form.Label>
                                <Form.Control as="textarea" placeholder="Your answer..." required
                                    onChange={(event: React.ChangeEvent<HTMLTextAreaElement>) => this.setAnswer(event.target.value)} />
                            </Form.Group>
                            <Button type="submit" variant="primary">
                                Post answer
                            </Button>
                        </Form>
                    )}
                </AnswerQuestionComponent>
                
            </div>
        );
    }
}

export default PostAnswer;
