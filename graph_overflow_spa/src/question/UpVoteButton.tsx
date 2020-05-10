import React from "react";
import { Button } from "react-bootstrap";

interface UpVoteButtonProperties {
    questionId: number;
    upVotes: number;
}
interface UpVoteButtonState {
}

class UpVoteButton extends React.Component<UpVoteButtonProperties, UpVoteButtonState> {
    render() {
        return (
            <Button variant="success">
                UpVote
            </Button>
        );
    }
}

export default UpVoteButton;
