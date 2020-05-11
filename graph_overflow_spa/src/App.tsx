import React from 'react';
import './App.css';
import { Navbar, Nav } from "react-bootstrap";

import { Route } from "react-router-dom";

import QuestionsList from "./frontpage/QuestionsList";

function App() {
  const questionsList = () => (<QuestionsList />);

  return (
    <div className="container">
      <Navbar bg="light" expand="lg">
					<Navbar.Brand href="/">GraphOverflow</Navbar.Brand>
        </Navbar>

      <Route exact path="/" component={questionsList} />
      <Route exact path="/questions" component={questionsList} />
    </div>
  );
}

export default App;
