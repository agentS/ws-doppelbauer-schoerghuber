import React from "react";
import { Button, Form } from "react-bootstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { AskQuestionComponent } from "../graphql/GraphQlTypes";

interface AskQuestionProperties extends RouteComponentProps {}
interface AskQuestionState {
    title: string,
    content: string
}

class AskQuestion extends React.Component<AskQuestionProperties, AskQuestionState> {
    constructor(properties: AskQuestionProperties) {
        super(properties);

        this.state = {
            title: "",
            content: ""
        };
    }

    setTitle(title: string) {
        this.setState({
            title
        });
    }

    setContent(content: string) {
        this.setState({
            content
        });
    }

    render() {
        return (
            <div>
                <h1>Ask a Question</h1>
                <AskQuestionComponent>
                    {mutate => (
                        <Form onSubmit={(event: React.FormEvent<HTMLFormElement>) => {
                            event.preventDefault();
                            mutate({ variables: {
                                title: this.state.title,
                                content: this.state.content,
                            }})
                                .then(result => {
                                    if (result.data && result.data.askQuestion) {
                                        this.props.history.push(`/question/${result.data.askQuestion.id}`);
                                    }
                                })
                                .catch(exception => {
                                    alert("Could not post the question.");
                                    console.error(exception);
                                });
                        }}>
                            <Form.Group controlId="askTitle">
                                <Form.Label>Question title</Form.Label>
                                <Form.Control type="text" placeholder="Title" required
                                    onChange={(event: React.ChangeEvent<HTMLInputElement>) => this.setTitle(event.target.value)}
                                />
                            </Form.Group>
                            <Form.Group controlId="askContent">
                                <Form.Label>Question</Form.Label>
                                <Form.Control as="textarea" placeholder="Question..." required
                                    onChange={(event: React.ChangeEvent<HTMLTextAreaElement>) => this.setContent(event.target.value)}
                                />
                            </Form.Group>
                            <Button variant="primary" type="submit">
                                Ask question
                            </Button>
                        </Form>
                    )}
                </AskQuestionComponent>
            </div>
        );
    }
}

export default withRouter(AskQuestion);
