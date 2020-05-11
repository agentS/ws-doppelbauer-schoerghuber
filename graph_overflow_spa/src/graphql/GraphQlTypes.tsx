import gql from 'graphql-tag';
import * as ApolloReactCommon from '@apollo/react-common';
import * as React from 'react';
import * as ApolloReactComponents from '@apollo/react-components';
import * as ApolloReactHoc from '@apollo/react-hoc';
export type Maybe<T> = T | null;
export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `Date` scalar type represents a year, month and day in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  Date: any;
  /** The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTime: any;
  /** The `DateTimeOffset` scalar type represents a date, time and offset from UTC. `DateTimeOffset` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTimeOffset: any;
  /** The `Seconds` scalar type represents a period of time represented as the total number of seconds. */
  Seconds: any;
  /** The `Milliseconds` scalar type represents a period of time represented as the total number of milliseconds. */
  Milliseconds: any;
  Decimal: any;
  Uri: any;
  Guid: any;
  Short: any;
  UShort: any;
  UInt: any;
  Long: any;
  BigInt: any;
  ULong: any;
  Byte: any;
  SByte: any;
};

















export type Query = {
   __typename?: 'Query';
  allTags: Array<Tag>;
  /** load all questions */
  latestQuestions: Array<Question>;
  me?: Maybe<User>;
  /** load a question */
  question: Question;
  /** get all tags that match the %tagName% */
  tags: Array<Tag>;
};


export type QueryQuestionArgs = {
  id?: Maybe<Scalars['Int']>;
};


export type QueryTagsArgs = {
  tagName?: Maybe<Scalars['String']>;
};

export type User = {
   __typename?: 'User';
  id: Scalars['ID'];
  name: Scalars['String'];
};

export type Tag = {
   __typename?: 'Tag';
  id: Scalars['ID'];
  name: Scalars['String'];
  questions: Array<Question>;
};

export type Question = PostInterface & {
   __typename?: 'Question';
  answers: Array<Answer>;
  content: Scalars['String'];
  createdAt: Scalars['DateTime'];
  id: Scalars['ID'];
  tags: Array<Tag>;
  title: Scalars['String'];
  upVotes: Scalars['Long'];
  upVoteUsers: Array<User>;
  user: User;
};

export type Answer = PostInterface & {
   __typename?: 'Answer';
  comments: Array<Comment>;
  content: Scalars['String'];
  createdAt: Scalars['DateTime'];
  id: Scalars['ID'];
  question: Question;
  upVotes: Scalars['Long'];
  upVoteUsers: Array<User>;
  user: User;
};

export type Comment = PostInterface & {
   __typename?: 'Comment';
  answer: Answer;
  content: Scalars['String'];
  createdAt: Scalars['DateTime'];
  id: Scalars['ID'];
  upVotes: Scalars['Long'];
  upVoteUsers: Array<User>;
  user: User;
};

export type PostInterface = {
  content: Scalars['String'];
  createdAt: Scalars['DateTime'];
  id: Scalars['ID'];
  upVotes: Scalars['Long'];
  upVoteUsers: Array<User>;
};

export type Mutation = {
   __typename?: 'Mutation';
  /** adds a graphoverflow tag */
  addTag?: Maybe<Tag>;
  /** adds an answer */
  answerQuestion?: Maybe<Answer>;
  /** adds a question */
  askQuestion?: Maybe<Question>;
  /** adds a comment */
  commentAnswer?: Maybe<Comment>;
  /** user login */
  login?: Maybe<AuthPayload>;
  /** upVote answer */
  upVoteAnswer?: Maybe<Answer>;
  /** upVote question */
  upVoteQuestion?: Maybe<Question>;
};


export type MutationAddTagArgs = {
  tagName: Scalars['String'];
};


export type MutationAnswerQuestionArgs = {
  questionId: Scalars['Int'];
  content: Scalars['String'];
};


export type MutationAskQuestionArgs = {
  question: QuestionInput;
};


export type MutationCommentAnswerArgs = {
  answerId: Scalars['Int'];
  content: Scalars['String'];
};


export type MutationLoginArgs = {
  loginData: UserLoginInput;
};


export type MutationUpVoteAnswerArgs = {
  answerId: Scalars['Int'];
};


export type MutationUpVoteQuestionArgs = {
  questionId: Scalars['Int'];
};

export type QuestionInput = {
  title: Scalars['String'];
  content: Scalars['String'];
};

export type AuthPayload = {
   __typename?: 'AuthPayload';
  token: Scalars['String'];
  user: User;
};

export type UserLoginInput = {
  userName: Scalars['String'];
  password: Scalars['String'];
};

export type Subscription = {
   __typename?: 'Subscription';
  tagAdded?: Maybe<Tag>;
};

export type AnswerQuestionMutationVariables = {
  questionId: Scalars['Int'];
  content: Scalars['String'];
};


export type AnswerQuestionMutation = (
  { __typename?: 'Mutation' }
  & { answerQuestion?: Maybe<(
    { __typename?: 'Answer' }
    & Pick<Answer, 'id'>
  )> }
);

export type AskQuestionMutationVariables = {
  title: Scalars['String'];
  content: Scalars['String'];
};


export type AskQuestionMutation = (
  { __typename?: 'Mutation' }
  & { askQuestion?: Maybe<(
    { __typename?: 'Question' }
    & Pick<Question, 'id'>
  )> }
);

export type LoginMutationVariables = {
  userName: Scalars['String'];
  password: Scalars['String'];
};


export type LoginMutation = (
  { __typename?: 'Mutation' }
  & { login?: Maybe<(
    { __typename?: 'AuthPayload' }
    & Pick<AuthPayload, 'token'>
  )> }
);

export type FetchQuestionQueryVariables = {
  questionId: Scalars['Int'];
};


export type FetchQuestionQuery = (
  { __typename?: 'Query' }
  & { question: (
    { __typename?: 'Question' }
    & Pick<Question, 'id' | 'title' | 'content' | 'createdAt' | 'upVotes'>
    & { upVoteUsers: Array<(
      { __typename?: 'User' }
      & Pick<User, 'id' | 'name'>
    )>, user: (
      { __typename?: 'User' }
      & Pick<User, 'name'>
    ), answers: Array<(
      { __typename?: 'Answer' }
      & Pick<Answer, 'id' | 'content' | 'createdAt' | 'upVotes'>
      & { upVoteUsers: Array<(
        { __typename?: 'User' }
        & Pick<User, 'id' | 'name'>
      )>, user: (
        { __typename?: 'User' }
        & Pick<User, 'id' | 'name'>
      ), comments: Array<(
        { __typename?: 'Comment' }
        & Pick<Comment, 'id' | 'content' | 'createdAt' | 'upVotes'>
        & { user: (
          { __typename?: 'User' }
          & Pick<User, 'id' | 'name'>
        ) }
      )> }
    )> }
  ) }
);

export type LatestQuestionsQueryVariables = {};


export type LatestQuestionsQuery = (
  { __typename?: 'Query' }
  & { latestQuestions: Array<(
    { __typename?: 'Question' }
    & Pick<Question, 'id' | 'title' | 'upVotes'>
  )> }
);

export type PostCommentMutationVariables = {
  answerId: Scalars['Int'];
  content: Scalars['String'];
};


export type PostCommentMutation = (
  { __typename?: 'Mutation' }
  & { commentAnswer?: Maybe<(
    { __typename?: 'Comment' }
    & Pick<Comment, 'id' | 'content' | 'createdAt'>
    & { user: (
      { __typename?: 'User' }
      & Pick<User, 'id' | 'name'>
    ) }
  )> }
);

export type UpVoteAnswerMutationVariables = {
  answerId: Scalars['Int'];
};


export type UpVoteAnswerMutation = (
  { __typename?: 'Mutation' }
  & { upVoteAnswer?: Maybe<(
    { __typename?: 'Answer' }
    & Pick<Answer, 'id' | 'upVotes'>
    & { upVoteUsers: Array<(
      { __typename?: 'User' }
      & Pick<User, 'id' | 'name'>
    )> }
  )> }
);

export type UpVoteQuestionMutationVariables = {
  questionId: Scalars['Int'];
};


export type UpVoteQuestionMutation = (
  { __typename?: 'Mutation' }
  & { upVoteQuestion?: Maybe<(
    { __typename?: 'Question' }
    & Pick<Question, 'id' | 'upVotes'>
    & { upVoteUsers: Array<(
      { __typename?: 'User' }
      & Pick<User, 'id' | 'name'>
    )> }
  )> }
);


export const AnswerQuestionDocument = gql`
    mutation answerQuestion($questionId: Int!, $content: String!) {
  answerQuestion(questionId: $questionId, content: $content) {
    id
  }
}
    `;
export type AnswerQuestionMutationFn = ApolloReactCommon.MutationFunction<AnswerQuestionMutation, AnswerQuestionMutationVariables>;
export type AnswerQuestionComponentProps = Omit<ApolloReactComponents.MutationComponentOptions<AnswerQuestionMutation, AnswerQuestionMutationVariables>, 'mutation'>;

    export const AnswerQuestionComponent = (props: AnswerQuestionComponentProps) => (
      <ApolloReactComponents.Mutation<AnswerQuestionMutation, AnswerQuestionMutationVariables> mutation={AnswerQuestionDocument} {...props} />
    );
    
export type AnswerQuestionProps<TChildProps = {}, TDataName extends string = 'mutate'> = {
      [key in TDataName]: ApolloReactCommon.MutationFunction<AnswerQuestionMutation, AnswerQuestionMutationVariables>
    } & TChildProps;
export function withAnswerQuestion<TProps, TChildProps = {}, TDataName extends string = 'mutate'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  AnswerQuestionMutation,
  AnswerQuestionMutationVariables,
  AnswerQuestionProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withMutation<TProps, AnswerQuestionMutation, AnswerQuestionMutationVariables, AnswerQuestionProps<TChildProps, TDataName>>(AnswerQuestionDocument, {
      alias: 'answerQuestion',
      ...operationOptions
    });
};
export type AnswerQuestionMutationResult = ApolloReactCommon.MutationResult<AnswerQuestionMutation>;
export type AnswerQuestionMutationOptions = ApolloReactCommon.BaseMutationOptions<AnswerQuestionMutation, AnswerQuestionMutationVariables>;
export const AskQuestionDocument = gql`
    mutation askQuestion($title: String!, $content: String!) {
  askQuestion(question: {title: $title, content: $content}) {
    id
  }
}
    `;
export type AskQuestionMutationFn = ApolloReactCommon.MutationFunction<AskQuestionMutation, AskQuestionMutationVariables>;
export type AskQuestionComponentProps = Omit<ApolloReactComponents.MutationComponentOptions<AskQuestionMutation, AskQuestionMutationVariables>, 'mutation'>;

    export const AskQuestionComponent = (props: AskQuestionComponentProps) => (
      <ApolloReactComponents.Mutation<AskQuestionMutation, AskQuestionMutationVariables> mutation={AskQuestionDocument} {...props} />
    );
    
export type AskQuestionProps<TChildProps = {}, TDataName extends string = 'mutate'> = {
      [key in TDataName]: ApolloReactCommon.MutationFunction<AskQuestionMutation, AskQuestionMutationVariables>
    } & TChildProps;
export function withAskQuestion<TProps, TChildProps = {}, TDataName extends string = 'mutate'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  AskQuestionMutation,
  AskQuestionMutationVariables,
  AskQuestionProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withMutation<TProps, AskQuestionMutation, AskQuestionMutationVariables, AskQuestionProps<TChildProps, TDataName>>(AskQuestionDocument, {
      alias: 'askQuestion',
      ...operationOptions
    });
};
export type AskQuestionMutationResult = ApolloReactCommon.MutationResult<AskQuestionMutation>;
export type AskQuestionMutationOptions = ApolloReactCommon.BaseMutationOptions<AskQuestionMutation, AskQuestionMutationVariables>;
export const LoginDocument = gql`
    mutation login($userName: String!, $password: String!) {
  login(loginData: {userName: $userName, password: $password}) {
    token
  }
}
    `;
export type LoginMutationFn = ApolloReactCommon.MutationFunction<LoginMutation, LoginMutationVariables>;
export type LoginComponentProps = Omit<ApolloReactComponents.MutationComponentOptions<LoginMutation, LoginMutationVariables>, 'mutation'>;

    export const LoginComponent = (props: LoginComponentProps) => (
      <ApolloReactComponents.Mutation<LoginMutation, LoginMutationVariables> mutation={LoginDocument} {...props} />
    );
    
export type LoginProps<TChildProps = {}, TDataName extends string = 'mutate'> = {
      [key in TDataName]: ApolloReactCommon.MutationFunction<LoginMutation, LoginMutationVariables>
    } & TChildProps;
export function withLogin<TProps, TChildProps = {}, TDataName extends string = 'mutate'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  LoginMutation,
  LoginMutationVariables,
  LoginProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withMutation<TProps, LoginMutation, LoginMutationVariables, LoginProps<TChildProps, TDataName>>(LoginDocument, {
      alias: 'login',
      ...operationOptions
    });
};
export type LoginMutationResult = ApolloReactCommon.MutationResult<LoginMutation>;
export type LoginMutationOptions = ApolloReactCommon.BaseMutationOptions<LoginMutation, LoginMutationVariables>;
export const FetchQuestionDocument = gql`
    query fetchQuestion($questionId: Int!) {
  question(id: $questionId) {
    id
    title
    content
    createdAt
    upVotes
    upVoteUsers {
      id
      name
    }
    user {
      name
    }
    answers {
      id
      content
      createdAt
      upVotes
      upVoteUsers {
        id
        name
      }
      user {
        id
        name
      }
      comments {
        id
        content
        createdAt
        upVotes
        user {
          id
          name
        }
      }
    }
  }
}
    `;
export type FetchQuestionComponentProps = Omit<ApolloReactComponents.QueryComponentOptions<FetchQuestionQuery, FetchQuestionQueryVariables>, 'query'> & ({ variables: FetchQuestionQueryVariables; skip?: boolean; } | { skip: boolean; });

    export const FetchQuestionComponent = (props: FetchQuestionComponentProps) => (
      <ApolloReactComponents.Query<FetchQuestionQuery, FetchQuestionQueryVariables> query={FetchQuestionDocument} {...props} />
    );
    
export type FetchQuestionProps<TChildProps = {}, TDataName extends string = 'data'> = {
      [key in TDataName]: ApolloReactHoc.DataValue<FetchQuestionQuery, FetchQuestionQueryVariables>
    } & TChildProps;
export function withFetchQuestion<TProps, TChildProps = {}, TDataName extends string = 'data'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  FetchQuestionQuery,
  FetchQuestionQueryVariables,
  FetchQuestionProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withQuery<TProps, FetchQuestionQuery, FetchQuestionQueryVariables, FetchQuestionProps<TChildProps, TDataName>>(FetchQuestionDocument, {
      alias: 'fetchQuestion',
      ...operationOptions
    });
};
export type FetchQuestionQueryResult = ApolloReactCommon.QueryResult<FetchQuestionQuery, FetchQuestionQueryVariables>;
export const LatestQuestionsDocument = gql`
    query latestQuestions {
  latestQuestions {
    id
    title
    upVotes
  }
}
    `;
export type LatestQuestionsComponentProps = Omit<ApolloReactComponents.QueryComponentOptions<LatestQuestionsQuery, LatestQuestionsQueryVariables>, 'query'>;

    export const LatestQuestionsComponent = (props: LatestQuestionsComponentProps) => (
      <ApolloReactComponents.Query<LatestQuestionsQuery, LatestQuestionsQueryVariables> query={LatestQuestionsDocument} {...props} />
    );
    
export type LatestQuestionsProps<TChildProps = {}, TDataName extends string = 'data'> = {
      [key in TDataName]: ApolloReactHoc.DataValue<LatestQuestionsQuery, LatestQuestionsQueryVariables>
    } & TChildProps;
export function withLatestQuestions<TProps, TChildProps = {}, TDataName extends string = 'data'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  LatestQuestionsQuery,
  LatestQuestionsQueryVariables,
  LatestQuestionsProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withQuery<TProps, LatestQuestionsQuery, LatestQuestionsQueryVariables, LatestQuestionsProps<TChildProps, TDataName>>(LatestQuestionsDocument, {
      alias: 'latestQuestions',
      ...operationOptions
    });
};
export type LatestQuestionsQueryResult = ApolloReactCommon.QueryResult<LatestQuestionsQuery, LatestQuestionsQueryVariables>;
export const PostCommentDocument = gql`
    mutation postComment($answerId: Int!, $content: String!) {
  commentAnswer(answerId: $answerId, content: $content) {
    id
    content
    createdAt
    user {
      id
      name
    }
  }
}
    `;
export type PostCommentMutationFn = ApolloReactCommon.MutationFunction<PostCommentMutation, PostCommentMutationVariables>;
export type PostCommentComponentProps = Omit<ApolloReactComponents.MutationComponentOptions<PostCommentMutation, PostCommentMutationVariables>, 'mutation'>;

    export const PostCommentComponent = (props: PostCommentComponentProps) => (
      <ApolloReactComponents.Mutation<PostCommentMutation, PostCommentMutationVariables> mutation={PostCommentDocument} {...props} />
    );
    
export type PostCommentProps<TChildProps = {}, TDataName extends string = 'mutate'> = {
      [key in TDataName]: ApolloReactCommon.MutationFunction<PostCommentMutation, PostCommentMutationVariables>
    } & TChildProps;
export function withPostComment<TProps, TChildProps = {}, TDataName extends string = 'mutate'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  PostCommentMutation,
  PostCommentMutationVariables,
  PostCommentProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withMutation<TProps, PostCommentMutation, PostCommentMutationVariables, PostCommentProps<TChildProps, TDataName>>(PostCommentDocument, {
      alias: 'postComment',
      ...operationOptions
    });
};
export type PostCommentMutationResult = ApolloReactCommon.MutationResult<PostCommentMutation>;
export type PostCommentMutationOptions = ApolloReactCommon.BaseMutationOptions<PostCommentMutation, PostCommentMutationVariables>;
export const UpVoteAnswerDocument = gql`
    mutation upVoteAnswer($answerId: Int!) {
  upVoteAnswer(answerId: $answerId) {
    id
    upVotes
    upVoteUsers {
      id
      name
    }
  }
}
    `;
export type UpVoteAnswerMutationFn = ApolloReactCommon.MutationFunction<UpVoteAnswerMutation, UpVoteAnswerMutationVariables>;
export type UpVoteAnswerComponentProps = Omit<ApolloReactComponents.MutationComponentOptions<UpVoteAnswerMutation, UpVoteAnswerMutationVariables>, 'mutation'>;

    export const UpVoteAnswerComponent = (props: UpVoteAnswerComponentProps) => (
      <ApolloReactComponents.Mutation<UpVoteAnswerMutation, UpVoteAnswerMutationVariables> mutation={UpVoteAnswerDocument} {...props} />
    );
    
export type UpVoteAnswerProps<TChildProps = {}, TDataName extends string = 'mutate'> = {
      [key in TDataName]: ApolloReactCommon.MutationFunction<UpVoteAnswerMutation, UpVoteAnswerMutationVariables>
    } & TChildProps;
export function withUpVoteAnswer<TProps, TChildProps = {}, TDataName extends string = 'mutate'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  UpVoteAnswerMutation,
  UpVoteAnswerMutationVariables,
  UpVoteAnswerProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withMutation<TProps, UpVoteAnswerMutation, UpVoteAnswerMutationVariables, UpVoteAnswerProps<TChildProps, TDataName>>(UpVoteAnswerDocument, {
      alias: 'upVoteAnswer',
      ...operationOptions
    });
};
export type UpVoteAnswerMutationResult = ApolloReactCommon.MutationResult<UpVoteAnswerMutation>;
export type UpVoteAnswerMutationOptions = ApolloReactCommon.BaseMutationOptions<UpVoteAnswerMutation, UpVoteAnswerMutationVariables>;
export const UpVoteQuestionDocument = gql`
    mutation upVoteQuestion($questionId: Int!) {
  upVoteQuestion(questionId: $questionId) {
    id
    upVotes
    upVoteUsers {
      id
      name
    }
  }
}
    `;
export type UpVoteQuestionMutationFn = ApolloReactCommon.MutationFunction<UpVoteQuestionMutation, UpVoteQuestionMutationVariables>;
export type UpVoteQuestionComponentProps = Omit<ApolloReactComponents.MutationComponentOptions<UpVoteQuestionMutation, UpVoteQuestionMutationVariables>, 'mutation'>;

    export const UpVoteQuestionComponent = (props: UpVoteQuestionComponentProps) => (
      <ApolloReactComponents.Mutation<UpVoteQuestionMutation, UpVoteQuestionMutationVariables> mutation={UpVoteQuestionDocument} {...props} />
    );
    
export type UpVoteQuestionProps<TChildProps = {}, TDataName extends string = 'mutate'> = {
      [key in TDataName]: ApolloReactCommon.MutationFunction<UpVoteQuestionMutation, UpVoteQuestionMutationVariables>
    } & TChildProps;
export function withUpVoteQuestion<TProps, TChildProps = {}, TDataName extends string = 'mutate'>(operationOptions?: ApolloReactHoc.OperationOption<
  TProps,
  UpVoteQuestionMutation,
  UpVoteQuestionMutationVariables,
  UpVoteQuestionProps<TChildProps, TDataName>>) {
    return ApolloReactHoc.withMutation<TProps, UpVoteQuestionMutation, UpVoteQuestionMutationVariables, UpVoteQuestionProps<TChildProps, TDataName>>(UpVoteQuestionDocument, {
      alias: 'upVoteQuestion',
      ...operationOptions
    });
};
export type UpVoteQuestionMutationResult = ApolloReactCommon.MutationResult<UpVoteQuestionMutation>;
export type UpVoteQuestionMutationOptions = ApolloReactCommon.BaseMutationOptions<UpVoteQuestionMutation, UpVoteQuestionMutationVariables>;