import React from "react";
import ReactDOM from "react-dom";
import "bootstrap/dist/css/bootstrap.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";

import { BrowserRouter as Router } from "react-router-dom";

import { ApolloClient } from "apollo-client";
import { InMemoryCache } from "apollo-cache-inmemory";
import { createHttpLink } from "apollo-link-http";
import { setContext } from "apollo-link-context";
import { ApolloProvider } from "@apollo/react-hooks";

import { getLoginToken } from "./authentication/AuthenticationUtils";


const cache = new InMemoryCache();
const httpLink = createHttpLink({
	uri: "http://127.0.0.1:5000/api/graphql/"
});
const authenticationLink = setContext((_, { headers }) => {
	const token = getLoginToken();
	return {
		headers: {
			...headers,
			authorization: token ? token : "",
		}
	};
});
const client = new ApolloClient({
	cache,
	link: authenticationLink.concat(httpLink)
});

ReactDOM.render(
	<React.StrictMode>
		<Router>
			<ApolloProvider client={client}>
				<App />
			</ApolloProvider>
		</Router>
	</React.StrictMode>,
	document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
