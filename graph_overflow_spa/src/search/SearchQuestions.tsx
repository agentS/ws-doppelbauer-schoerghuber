import React from "react";
import { ListGroup, Col, Row, Form, Badge } from "react-bootstrap";

import { SearchByTagNameComponent, Tag } from "../graphql/GraphQlTypes";

interface QuestionsListProperties {}
interface SearchQuestionsState {
  tagName: string,
  tags: (Tag[] | null)
}

class SearchQuestions extends React.Component<QuestionsListProperties, SearchQuestionsState>{
  constructor(properties: QuestionsListProperties) {
    super(properties);

    this.state = {
      tagName: "",
      tags: null
    };
  }

  setTagName(tagName: string) {
    this.setState({
      tagName
    });
  }

	render(){
    return (
        <div>
        <Form onSubmit={(event: React.FormEvent<HTMLFormElement>) => {
            event.preventDefault();
        }}>
            <Form.Group controlId="searchBar">
                <Form.Control type="text" placeholder="TagName" required
                    onChange={(event: React.ChangeEvent<HTMLInputElement>) => this.setTagName(event.target.value)}
                />
            </Form.Group>
        </Form>
        {
          () => {
            if(this.state.tagName !== ""){
              console.log("tagName is empty");
            }
          }
        }
        <SearchByTagNameComponent variables={{tagName: this.state.tagName}}>
          {({ error, data }) => {
					if (error || !data) {
						return (<h3>An error occured while loading the questions</h3>);
					}
					return (
            <ListGroup>
							{data.questionsByTag.map(question => (
								<ListGroup.Item key={question.id}
									action href={`/question/${question.id}`}
								>
									<Row>
										<Col xs={2}>{question.upVotes} Upvotes</Col>
										<Col>{question.title}</Col>
										<Col>{question.tags.map(t => <span><Badge variant="dark">{t.name}</Badge>{' '}</span>)}</Col>
									</Row>
								</ListGroup.Item>
							))}
						</ListGroup>
					);
				}}
        </SearchByTagNameComponent> 
        </div>
    );
  }
}

export default SearchQuestions;