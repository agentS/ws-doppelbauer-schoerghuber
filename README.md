# Aufgabenstellung

Alex

# Architektur

Alex

Unsere Anwendung ist als Client-Server-System realisiert.
Der Server ist dabei mit dem ASP.NET Core und [GraphQL Server](https://github.com/graphql-dotnet/server/) realisiert, während der Client als SPA mit [React](https://www.reactjs.org/), TypeScript und dem [Apollo-Client für React](https://www.apollographql.com/docs/react/) entwickelt ist.



# Setup

Da uns das Setup der Projekte einige Probleme bereitet hat, möchten wir in diesem Abschnitt darauf eingehen, wie wir zu einer für uns funktionierenden Konfiguration gelangt sind.

## GraphQL Server

Alex

## Apollo-React-Client

### NPM-Packages

Die Installation des Apollo-Clients erfordert zusätzliche Packages, was in der Dokumentation leider nicht allzu gut beschrieben ist.
Insbesondere für die Subscriptions sind zusätzliche Packages erforderlich.
Das unten stehende Kommando listet alle Pakete auf, die für den Apollo-React-Client benötigt werden.

```bash
npm install --save apollo-boost @apollo/react-hooks graphql apollo-cache-inmemory apollo-client apollo-link-http apollo-link-ws graphql-tag subscriptions-transport-ws
```

### GraphQL-Client-Setup

Der nächste Schritt besteht darin, den Apollo-Client in der React-Anwendung verfügbar zu machen.
Hierzu ist in der Datei `index.tsx`, welche vom [React-Projekt-Generator](https://reactjs.org/docs/create-a-new-react-app.html#create-react-app) erzeugt wird, eine Instanz des Clients anzulegen.

Jede Instanz benötigt hierzu einen Link und einen Cache.
Der Cache wird einfach als In-Memory-Cache realisiert und kann mit derunten zu sehenden Anweisung erzeugt werden.

```typescript
const cache = new InMemoryCache();
```

Der Apollo-Link repräsentiert die Verbindung zum GraphQL-Server.
Das Apollo-Framework ermöglicht es dabei Links wie Middleware-Komponenten in ASP.NET Core als Kette zu repräsentieren.
Dies wird von uns genutzt, um Queries sowie Mutations über einen HTTP-Link mit Authentifizierung auszuführen, während die Daten für Subscriptions über einen dedizierten Web-Socket-Link übertragen werden.

Das untere Snippet zeigt den Code zur Erstellung des Http-Links sowie eines Middleware-Providers, welcher den Authentifizierungs-Token, sofern dieser vorhanden ist, aus dem Local-Storage in den HTTP-Header kopiert.

```typescript
const httpLink = createHttpLink({
	uri: "http://127.0.0.1:5000/api/graphql/"
});
const authenticationLink = setContext((_, { headers }) => {
	const token = getLoginToken();
	return {
		headers: {
			...headers,
			authorization: token ? token : "",
		}
	};
});
```

Der unten zu sehende Quellcodeauszug zeigt den Code, welcher zur Erstellung des Web-Socket-Links für Subscriptions erforderlich ist.
Zusätzlich wird mittels eines Splits festgelegt, dass Subscriptions vom Web-Socket-Link verarbeitet werden, während der HTTP-Link mit verkettetem Authentifizierungs-Handler für alle übrigen Operationen verwendet wird.

```typescript
const webSocketLink = new WebSocketLink({
	uri: "ws://127.0.0.1:5000/api/graphql",
	options: {
		reconnect: true
	}
});
const apolloLink = split(
	({ query }) => {
		const definition = getMainDefinition(query);
		return definition.kind === "OperationDefinition" && definition.operation === "subscription";
	},
	webSocketLink,
	authenticationLink.concat(httpLink)
);
```

Abschließend wird ein Apollo-Client instanziiert und als React-Hook den React-Komponenten zur Verfügung gestellt.
Um letzteres zu ermöglichen, wird der instanziierte Client der React-Componente `<ApolloProvider>` als Property übergeben.

```typescript
const client = new ApolloClient({
	cache,
	link: apolloLink
});

ReactDOM.render(
	<React.StrictMode>
		<Router>
			<ApolloProvider client={client}>
				<App />
			</ApolloProvider>
		</Router>
	</React.StrictMode>,
	document.getElementById("root")
);
```

### GraphQL-Client-Generator

Mit der bisherigen Konfiguration kann nun die von Apollo zur Verfügung gestellte Funktionalität vollständig genutzt werden.
Allerdings werden von Apollo anhand der GraphQL-Operationen und des vom Server bereitgestellten Schemas noch keine DTOs zur typsicheren Repräsentation der Daten generiert.

Um die Generierung von DTOs zu ermöglichen, wird das Projekt [GraphQL-Code-Generator](https://github.com/dotansimha/graphql-code-generator) eingesetzt.
Darüber hinaus bietet dieses Projekt den Vorteil, dass ebenfalls React-Komponenten, welche eine Operation repräsentieren, anhand von GraphQL-Abfragen generiert werden können.

Um den Generator verwenden zu können, sind die folgenden Pakete als Entwicklungsumgebungs-Abhängigkeiten mit dem unten ersichtlichen Kommando zu installieren.

```bash
npm install --save-dev @graphql-codegen/cli
```

Anschließend ist der Generator noch zu initialiseren, was mit der Anweisung `npx graphql-codegen init
` erfolgt.
Als Applikationstyp ist dabei die bereits vorgeschlagene Option `Application built with React` auszuwählen.
Als Schema-URL ist der GraphQL-POST-Endpunkt, der bereits zur Konfiguration des Apollo-Clients festgelegt wurde, einzugeben, nämlich `http://127.0.0.1:5000/api/graphql`.
Bei der Frage, wo die Dateien mit den GraphQL-Operationen und -Fragmenten liegen, kann der vorgeschlagene Wert `src/**/*.graphql` beibehalten werden.
Bei den zu verwendenden Plugins können ebenfalls die vorgeschlagenen Plugins `TypeScript`, `TypeScript Operations` und `TypeScript React Apollo` ausgewählt werden.
Als Datei, in welche die generierten Typen gespeichert werden, haben wir `src/graphql/GraphQlTypes.tsx` ausgewählt.
Auf die Generierung einer Introspection-Datei haben wir verzichtet.
Als Namen für die Konfigurationsdatei des Generators wird der vorgeschlagene Name `codegen.yml` verwendet.
Abschließend wird als Eingabe für den Namen des Befehls zur Ausführung des Codegenerator `codegen` eingegeben.
Die finale Ausgabe zeigt dann noch die Befehle zur Installation der Plugins sowie zur Ausführung des Codegenerators an.

Es wurde, den [offiziellen Code-Generator von Apollo](https://github.com/apollographql/apollo-tooling) zu verwenden, allerdings mit eher bescheidenem Erfolg: Auf der offiziellen Projektseite ist keine wirkliche Anleitung vorzufinden und die [Anweisungen in Blog-Posts](https://www.leighhalliday.com/generating-types-apollo) haben nicht funktioniert.

# Schema

Alex

# Queries

## Server

Alex

## Client

Lukas

# Mutations

## Server

Alex

## Client

Lukas

# Subscriptions

Lukas

## Server

Lukas

## Client

Lukas

# Data-Loader

Lukas

# Ergebnisse

Lukas
