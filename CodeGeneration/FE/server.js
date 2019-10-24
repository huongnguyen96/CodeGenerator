#!/usr/bin/env node

const express = require("express");
const {createServer} = require("http");

const PORT = 3000;

const app = express();
app.use(express.json());
app.use(express.urlencoded({
  extended: true,
}));

app.use(express.static("build"));
app.use((req, res) => {
  res.redirect('/');
});

const server = createServer(app);
server.listen(PORT, () => {
  console.log("Web server is up and running on port %d", PORT);
});
