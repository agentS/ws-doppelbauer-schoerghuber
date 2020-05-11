export type Maybe<T> = T | null;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
	ID: string;
	String: string;
	Boolean: boolean;
	Int: number;
	Float: number;
	BigInt: any;
	Byte: any;
	Date: any;
	DateTime: any;
	DateTimeOffset: any;
	Decimal: any;
	Guid: any;
	Long: any;
	Milliseconds: any;
	SByte: any;
	Seconds: any;
	Short: any;
	UInt: any;
	ULong: any;
	Uri: any;
	UShort: any;
};

export type Answer = PostInterface & {
	 __typename?: 'Answer';
	comments: Array<Comment>;
	content: Scalars['String'];
	createdAt: Scalars['DateTime'];
	id: Scalars['ID'];
	question: Question;
	upVotes: Scalars['Int'];
};



export type Comment = PostInterface & {
	 __typename?: 'Comment';
	answer: Answer;
	content: Scalars['String'];
	createdAt: Scalars['DateTime'];
	id: Scalars['ID'];
	upVotes: Scalars['Int'];
};








export type Mutation = {
	 __typename?: 'Mutation';
	addQuestion?: Maybe<Question>;
	addTag?: Maybe<Tag>;
};


export type MutationAddQuestionArgs = {
	question: QuestionInput;
};


export type MutationAddTagArgs = {
	tagName: Scalars['String'];
};

export type PostInterface = {
	content: Scalars['String'];
	createdAt: Scalars['DateTime'];
	id: Scalars['ID'];
	upVotes: Scalars['Int'];
};

export type Query = {
	 __typename?: 'Query';
	allTags: Array<Tag>;
	hello?: Maybe<Scalars['String']>;
	latestQuestions: Array<Question>;
	name?: Maybe<Scalars['String']>;
	tags: Array<Tag>;
};


export type QueryTagsArgs = {
	tagName?: Maybe<Scalars['String']>;
};

export type Question = PostInterface & {
	 __typename?: 'Question';
	answers: Array<Answer>;
	content: Scalars['String'];
	createdAt: Scalars['DateTime'];
	id: Scalars['ID'];
	tags: Array<Tag>;
	title: Scalars['String'];
	upVotes: Scalars['Int'];
};

export type QuestionInput = {
	title: Scalars['String'];
	content: Scalars['String'];
};




export type Subscription = {
	 __typename?: 'Subscription';
	tagAdded?: Maybe<Tag>;
};

export type Tag = {
	 __typename?: 'Tag';
	id: Scalars['ID'];
	name: Scalars['String'];
	questions: Array<Question>;
};

