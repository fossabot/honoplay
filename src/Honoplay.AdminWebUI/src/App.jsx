import React, { Component } from 'react';
import { hot } from 'react-hot-loader';
import { Route } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';

import Questions from './views/Questions/Questions';
import TenantInformation from './views/TenantInformation/TenantInformation';


class App extends Component {
    render() {
        return (
            <Layout>             
                <Route path="/home/sorular" component={Questions} />    
                <Route path="/home/sirketbilgileri" component={TenantInformation}/>  
            </Layout>
        );
    }
}

export default hot(module)(App);