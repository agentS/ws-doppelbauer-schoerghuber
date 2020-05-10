import React from 'react';
import './App.css';

import { Route } from "react-router-dom";

import QuestionsList from "./frontpage/QuestionsList";

function App() {
  const questionsList = () => (<QuestionsList />);

  return (
    <div className="container">
      <Route exact path="/" component={questionsList} />
      <Route exact path="/questions" component={questionsList} />
    </div>
  );
}

export default App;
