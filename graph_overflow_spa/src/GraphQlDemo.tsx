import React, { Fragment } from "react";

import { useQuery } from "@apollo/react-hooks";
import gql from "graphql-tag";


interface DemoProperties {}

const GraphQlDemo: React.FC<DemoProperties> = () => {
	const {
		data,
		loading,
		error
	} = useQuery(gql`
		query {
		hello
		}
	`);

	if (loading) {
		return <p>"Loading..."</p>;
	}
	if (error) {
		return <h1>Simpson!</h1>;
	}
	if (!data) {
		return <h1>Not found</h1>;
	}

	return (
		<h1>{data.hello}</h1>
	);
};

export default GraphQlDemo;
