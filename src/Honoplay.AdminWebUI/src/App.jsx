import React, { Component } from 'react';
import { hot } from 'react-hot-loader';
import { Route } from 'react-router-dom';
import Layout from './components/Layout/LayoutComponent';

import Sorular from './views/Sorular/Sorular';
import SirketBilgileri from './views/SirketBilgileri/SirketBilgileri';

class App extends Component {
    render() {
        return (
            <Layout>             
                <Route path="/home/sorular" component={Sorular} />    
                <Route path="/home/sirketbilgileri" component={SirketBilgileri}/>       
            </Layout>
        );
    }
}

export default hot(module)(App);