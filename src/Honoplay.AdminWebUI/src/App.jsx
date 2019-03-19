import React, { Component } from "react";
import { hot } from "react-hot-loader";
import {BrowserRouter as Router, Route } from 'react-router-dom';
import Sorular from "./views/Sorular";
import EgitimSerisi from "./views/EgitimSerisi";
import Layout from "./components/Layout";




class App extends Component {
    render() {
        return (
            <Router>
                <Layout>             
                    <Route exact path="/" render={() => <h2>Home</h2>}/>
                    <Route path="/sorular" component={Sorular} />
                    <Route path="/egitimserisi" component={EgitimSerisi} /> 
                </Layout>
            </Router>
        );
    }
}

export default hot(module)(App);