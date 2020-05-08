import React, { Fragment } from "react";

import { useQuery } from "@apollo/react-hooks";
import gql from "graphql-tag";

import { Question } from "../graphql/GraphQlSchemaTypes";

import QuestionDisplay from "./QuestionDisplay";

interface QuestionsListProperties {}

const QuestionsList: React.FC<QuestionsListProperties> = () => {
	const {
		data,
		loading,
		error
	} = useQuery(gql`
		query latestQuestions{
			latestQuestions {
				id
				title
				content
				createdAt
				upVotes
			}
		}
	`);

	if (loading) {
		return (<h1>Fetching latest questions</h1>);
	}
	if (error) {
		return (<h1>An error occured on fetching the latest questions</h1>);
	}
	if (!data) {
		return (<h1>Not found</h1>);
	}

	return (
		<div>
			<h1>Latest Questions</h1>
			{data.latestQuestions.map((question: Question) => (
				<QuestionDisplay question={question} />
			))}
		</div>
	)
};

export default QuestionsList;
