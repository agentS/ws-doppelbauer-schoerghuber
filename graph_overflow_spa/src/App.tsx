import React from 'react';
import './App.css';
import { withRouter, RouteComponentProps } from "react-router-dom";
import { Navbar, Nav } from "react-bootstrap";

import { Route } from "react-router-dom";

import { isLoggedIn, clearLoginToken } from "./authentication/AuthenticationUtils"; 

import SearchQuestions from "./search/SearchQuestions";
import AskQuestion from "./ask/AskQuestion";
import QuestionsList from "./frontpage/QuestionsList";
import QuestionPage from "./question/QuestionPage";
import Login from "./authentication/Login";

interface AppProperties extends RouteComponentProps {}
interface AppState {}

class App extends React.Component<AppProperties, AppState> {

  logout() {
    clearLoginToken();
    this.props.history.push("/");
  }

  render() {
    const search = () => (<SearchQuestions />);
    const questionsList = () => (<QuestionsList />);
    const login = () => (<Login />);
    const askQuestion = () => (<AskQuestion />);
    const questionPage = ({match}: any) => (
      <QuestionPage questionId={match.params.questionId} />
    );

    return (
      <div className="container">
        <Navbar bg="light" expand="lg">
          <Navbar.Brand href="/">GraphOverflow</Navbar.Brand>
          <Nav.Link href="/search">Search</Nav.Link>
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
        <Route exact path="/question/:questionId" component={questionPage} />
        <Route exact path="/ask" component={askQuestion}/>
        <Route exact path="/login" component={login}/>
        <Route exact path="/search" component={search}/>
      </div>
    );
  }
}

export default withRouter(App);
