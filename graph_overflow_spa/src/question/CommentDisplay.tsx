import React from "react";

import { Comment } from "../graphql/GraphQlTypes";
import { formatDateTime } from "../DateTimeUtilities";

interface CommentDisplayProperties {
    comment: Comment;
}

interface CommentDisplayState {}

class CommentDisplay extends React.Component<CommentDisplayProperties, CommentDisplayState> {
    render() {
        return (
            <div key={this.props.comment.id}>
                <p>{this.props.comment.content}</p>
                <p>Created by {this.props.comment.user.name} at {formatDateTime(this.props.comment.createdAt)}</p>
                <hr />
            </div>
        );
    }
}

export default CommentDisplay;
