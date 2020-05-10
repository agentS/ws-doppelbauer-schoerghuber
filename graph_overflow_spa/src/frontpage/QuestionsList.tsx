import React from "react";
import { Accordion, Button, Card, Col, Row } from "react-bootstrap";

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
						<Accordion>
							{data.latestQuestions.map(question => (
								<Card key={question.id}>
									<Card.Header>
										<Accordion.Toggle as={Button} variant="link" eventKey={question.id}>
											<Row>
												<Col xs={6}>{question.upVotes} Upvote(s)</Col>
												<Col>{question.title}</Col>
											</Row>
										</Accordion.Toggle>
									</Card.Header>
									<Accordion.Collapse eventKey={question.id}>
										<Card.Body>
											{question.content}
										</Card.Body>
									</Accordion.Collapse>
								</Card>
							))}
						</Accordion>
					);
				}}
			</LatestQuestionsComponent>
		</div>
	)
};

export default QuestionsList;
