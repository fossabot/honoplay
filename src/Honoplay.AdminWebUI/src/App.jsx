import React, { Component } from "react";
import { hot } from "react-hot-loader";
import Layout from './components/Layout';
import "./styles/application.css";

class App extends Component {
    render() {
        return (
            <Layout/>
        );
    }
}

export default hot(module)(App);