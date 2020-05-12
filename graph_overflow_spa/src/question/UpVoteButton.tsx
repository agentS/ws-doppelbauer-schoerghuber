import React from "react";
import { Button } from "react-bootstrap";

import { User, UpVoteAnswerComponent, UpVoteQuestionComponent } from "../graphql/GraphQlTypes";

import { isLoggedIn, getUserId } from "../authentication/AuthenticationUtils";

export interface UpVoteCallback {
    (id: number, newUpVotes: number, newUpVoteUsers: User[]): void;
}

export enum UpVoteButtonMode {
    QUESTION,
    ANSWER
}

interface UpVoteButtonProperties {
    mode: UpVoteButtonMode;
    postId: number;
    upVoteUsers: User[];
    onUpvote: UpVoteCallback;
}
interface UpVoteButtonState {
}

class UpVoteButton extends React.Component<UpVoteButtonProperties, UpVoteButtonState> {
    render() {
        if (isLoggedIn()) {
            const userId = getUserId();
            if (
                (userId)
                && (!this.props.upVoteUsers.find(upVoteUser => parseInt(upVoteUser.id) === userId))
            ) {
                switch (this.props.mode) {
                case UpVoteButtonMode.QUESTION:
                    return (
                        <UpVoteQuestionComponent variables={{questionId: this.props.postId}}>
                            {mutate => 
                                <Button variant="success"
                                    onClick={() => {
                                        mutate()
                                            .then(result => {
                                                if (result.data && result.data.upVoteQuestion) {
                                                    this.props.onUpvote(
                                                        parseInt(result.data.upVoteQuestion.id),
                                                        result.data.upVoteQuestion.upVotes,
                                                        result.data.upVoteQuestion.upVoteUsers
                                                    );
                                                } else {
                                                    alert("Could not up vote.");
                                                    console.error(result.errors);
                                                }
                                            })
                                            .catch(exception => {
                                                alert("Could not up vote.");
                                                console.error(exception);
                                            });
                                    }}>
                                    UpVote
                                </Button>
                            }
                        </UpVoteQuestionComponent>
                    );
                case UpVoteButtonMode.ANSWER:
                    return (
                        <UpVoteAnswerComponent variables={{answerId: this.props.postId}}>
                            {mutate => 
                                <Button variant="success"
                                    onClick={() => {
                                        mutate()
                                            .then(result => {
                                                if (result.data && result.data.upVoteAnswer) {
                                                    this.props.onUpvote(
                                                        parseInt(result.data.upVoteAnswer.id),
                                                        result.data.upVoteAnswer.upVotes,
                                                        result.data.upVoteAnswer.upVoteUsers
                                                    );
                                                } else {
                                                    alert("Could not up vote.");
                                                    console.error(result.errors);
                                                }
                                            })
                                            .catch(exception => {
                                                alert("Could not up vote.");
                                                console.error(exception);
                                            });
                                    }}>
                                    UpVote
                                </Button>
                            }
                        </UpVoteAnswerComponent>
                    );
                }
            }
        }        
        return (<span></span>);
    }
}

export default UpVoteButton;
