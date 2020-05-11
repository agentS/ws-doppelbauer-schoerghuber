import React from "react";
import { Form, Button } from "react-bootstrap";
import { withRouter, RouteComponentProps } from "react-router-dom";

import gql from "graphql-tag";
import { useMutation } from "@apollo/react-hooks";
import { LoginMutationVariables, LoginMutationResult } from "../graphql/GraphQlTypes";

import { setLoginToken } from "./AuthenticationUtils";

interface LoginProperties extends RouteComponentProps {}

const Login: React.FC<LoginProperties> = (props) => {
    let username: string = "";
    let password: string = "";

    const [ loginMutation ] = useMutation(gql`
        mutation login($userName: String!, $password: String!) {
            login(loginData: {userName: $userName, password: $password}) {
                token
            }
        }
    `);

    return (
        <div>
            <h1>Login</h1>

            <Form onSubmit={(event: React.FormEvent<HTMLFormElement>) => {
                event.preventDefault();
                const variables: LoginMutationVariables = {
                    userName: username,
                    password
                }
                loginMutation({ variables })
                    .then(response => {
                        const loginResult = response as LoginMutationResult;
                        if (loginResult.data && loginResult.data.login) {
                            setLoginToken(loginResult.data?.login?.token);
                            props.history.push(`/`);
                        } else {
                            alert("Invalid credentials!");
                        }
                    })
                    .catch(exception => {
                        console.log(exception);
                    });
            }}>
                <Form.Group controlId="loginFormUsername">
                    <Form.Label>Username</Form.Label>
                    <Form.Control type="text" placeholder="Username" required
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => username = event.target.value}
                    />
                </Form.Group>
                <Form.Group controlId="loginFormPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" required
                        onChange={(event: React.ChangeEvent<HTMLInputElement>) => password = event.target.value}
                    />
                </Form.Group>
                <Button variant="primary" type="submit">
                    Login
                </Button>
            </Form>
        </div>
    );
};

export default withRouter(Login);
