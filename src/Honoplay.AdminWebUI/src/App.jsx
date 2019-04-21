import React, { Component } from 'react';
import { hot } from 'react-hot-loader';
import {BrowserRouter as Router, Route } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';

import Sorular from './views/Sorular/Sorular';

class App extends Component {
    render() {
        return (
            <Router>
                <Layout>             
                    <Route path="/home/sorular" component={Sorular} />         
                </Layout>
            </Router>
        );
    }
}

export default hot(module)(App);