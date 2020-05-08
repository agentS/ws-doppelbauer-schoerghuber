import React from 'react';
import './App.css';

import { Route } from "react-router-dom";

import QuestionsList from "./frontpage/QuestionsList";

function App() {
  const questionsList = () => (<QuestionsList />);

  return (
    <div className="App">
      <header className="App-header">
        <Route exact path="/" component={questionsList} />
        <Route exact path="/questions" component={questionsList} />
      </header>
    </div>
  );
}

export default App;
