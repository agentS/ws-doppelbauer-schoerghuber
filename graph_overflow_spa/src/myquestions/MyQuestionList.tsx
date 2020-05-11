import React from "react";
import { ListGroup, Row, Col } from "react-bootstrap";

import { getUserId } from "../authentication/AuthenticationUtils";
import { formatDateTime } from "../DateTimeUtilities";
import { MyQuestionsComponent } from "../graphql/GraphQlTypes";

interface MyQuestionListProperties {}
interface MyQuestionListState {}

class MyQuestionList extends React.Component<MyQuestionListProperties, MyQuestionListState> {
    render() {
        const userId = getUserId();
        if (userId) {
            return (
                <div>
                    <h1>My latest Questions</h1>
                    <MyQuestionsComponent variables={{ userId: userId }}>
                        {({ loading, error, data }) => {
                            if (loading) {
                                return (<h3>Loading questions...</h3>);
                            }
        
                            if (error || !data) {
                                return (<h3>An error occured while loading the questions</h3>);
                            }

                            return (
                                <ListGroup>
                                    {data.latestQuestions.map(question => (
                                        <ListGroup.Item key={question.id}
                                            action href={`/question/${question.id}`}
                                        >
                                            <h3>{question.title}</h3>
                                            <p>{question.content}</p>
                                            <p>Created at {formatDateTime(question.createdAt)}</p>
                                            <hr />
                                            <Row>
                                                <Col xs={2} />
                                                <Col><h5>Answers</h5></Col>
                                            </Row>
                                            { question.answers.map(answer => (
                                                <div key={answer.id}>
                                                    <Row>
                                                        <Col xs={2} />
                                                        <Col>
                                                            <p>{answer.content}</p>
                                                            <p>Created at {formatDateTime(answer.createdAt)}</p>
                                                        </Col>
                                                    </Row>
                                                    <hr />
                                                </div>
                                            )) }
                                        </ListGroup.Item>
                                    ))}
                                </ListGroup>
                            );
                        }}
                    </MyQuestionsComponent>
                </div>
            );
        }
        return (<h1>Please login first!</h1>);
    }
}

export default MyQuestionList;
