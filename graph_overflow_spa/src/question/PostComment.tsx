import React from "react";
import { Form, Button } from "react-bootstrap";

import { PostCommentComponent } from "../graphql/GraphQlTypes";

interface PostCommentProperties {
    answerId: number;
}

interface PostCommentState {
    content: string;
}

class PostComment extends React.Component<PostCommentProperties, PostCommentState> {
    constructor(properties: PostCommentProperties) {
        super(properties);

        this.state = {
            content: ""
        };
    }

    setContent(content: string) {
        this.setState({ content });
    }

    render() {
        return (
            <div>
                <h5>Post a Comment</h5>

                <PostCommentComponent>
                    {mutate => {
                        return (
                            <Form onSubmit={(event: React.FormEvent<HTMLFormElement>) => {
                                event.preventDefault();

                                mutate({ variables: {
                                    answerId: this.props.answerId,
                                    content: this.state.content,
                                }})
                                    .then(result => {
                                        alert("Comment posted");
                                    })
                                    .catch(exception => {
                                        alert("Could not post the comment.");
                                        console.error(exception);
                                    });
                            }}>
                                <Form.Group controlId="comment">
                                    <Form.Label>Your comment</Form.Label>
                                    <Form.Control as="textarea" placeholder="Your comment..." required
                                    onChange={(event: React.ChangeEvent<HTMLTextAreaElement>) => this.setContent(event.target.value)} />
                                </Form.Group>
                                <Button type="submit" variant="primary">
                                    Post comment
                                </Button>
                            </Form>
                        );
                    }}
                </PostCommentComponent>
            </div>
        )
    }
}

export default PostComment;
