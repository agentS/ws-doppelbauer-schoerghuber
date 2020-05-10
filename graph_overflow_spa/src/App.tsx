import React from 'react';
import './App.css';
import { withRouter, RouteComponentProps } from "react-router-dom";
import { Navbar, Nav } from "react-bootstrap";

import { Route } from "react-router-dom";

import { isLoggedIn, clearLoginToken } from "./authentication/AuthenticationUtils"; 

import AskQuestion from "./ask/AskQuestion";
import QuestionsList from "./frontpage/QuestionsList";
import QuestionDisplay from "./question/QuestionDisplay";
import Login from "./authentication/Login";

interface AppProperties extends RouteComponentProps {}
interface AppState {}

class App extends React.Component<AppProperties, AppState> {

  logout() {
    clearLoginToken();
    this.props.history.push("/");
  }

  render() {
    const questionsList = () => (<QuestionsList />);
    const login = () => (<Login />);
    const askQuestion = () => (<AskQuestion />);
    const questionDisplay = ({match}: any) => (
      <QuestionDisplay questionId={match.params.questionId} />
    );

    return (
      <div className="container">
        <Navbar bg="light" expand="lg">
          <Navbar.Brand href="/">GraphOverflow</Navbar.Brand>
          {
            isLoggedIn()
            ? (<Nav.Link href="/ask">Ask question</Nav.Link>)
            : (<span></span>)
          }
          {
            isLoggedIn()
            ? (<Nav.Link onClick={() => this.logout()}>Logout</Nav.Link>)
            : (<Nav.Link href="/login">Login</Nav.Link>)
          }
          
        </Navbar>

        <Route exact path="/" component={questionsList} />
        <Route exact path="/questions" component={questionsList} />
        <Route exact path="/question/:questionId" component={questionDisplay} />
        <Route exact path="/ask" component={askQuestion}/>
        <Route exact path="/login" component={login}/>
      </div>
    );
  }
}

export default withRouter(App);
