import React from "react";
import { ListGroup, Col, Row, Badge } from "react-bootstrap";

import { LatestQuestionsComponent } from "../graphql/GraphQlTypes";

interface QuestionsListProperties {}

const QuestionsList: React.FC<QuestionsListProperties> = () => {
	return (
		<div>
			<h1>Latest Questions</h1>
			<LatestQuestionsComponent>
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
									<Row>
										<Col xs={2}>{question.upVotes} Upvotes</Col>
										<Col>{question.title}</Col>
										<Col>{question.tags.map(t => <span><Badge variant="dark">{t.name}</Badge>{' '}</span>)}</Col>
									</Row>
								</ListGroup.Item>
							))}
						</ListGroup>
					);
				}}
			</LatestQuestionsComponent>
		</div>
	)
};

export default QuestionsList;
