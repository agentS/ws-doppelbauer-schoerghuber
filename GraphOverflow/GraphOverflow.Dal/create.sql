CREATE TABLE answer(
    id SERIAL PRIMARY KEY NOT NULL,
    title varchar(255),
    content text NOT NULL,
    created_at TIMESTAMP NOT NULL,
    up_votes INT DEFAULT 0,
    question_id INT,
    CONSTRAINT fk_answer_question FOREIGN KEY (question_id) REFERENCES answer(id)
);

CREATE TABLE comment(
    id SERIAL PRIMARY KEY NOT NULL,
    content text NOT NULL,
    created_at TIMESTAMP NOT NULL,
    answer_id INT,
    CONSTRAINT fk_comment_answer FOREIGN KEY (answer_id) REFERENCES answer(id)
);

CREATE TABLE tag(
    id SERIAL PRIMARY KEY NOT NULL,
    name varchar(255)
);

CREATE TABLE tag_answer(
    tag_id INT,
    answer_id INT,

    CONSTRAINT fk_tag_answer_answer FOREIGN KEY (answer_id) REFERENCES answer(id),
    CONSTRAINT fk_tag_answer_tag FOREIGN KEY (tag_id) REFERENCES tag(id),
    CONSTRAINT pk_tag_answer PRIMARY KEY(tag_id, answer_id)
);
commit;
