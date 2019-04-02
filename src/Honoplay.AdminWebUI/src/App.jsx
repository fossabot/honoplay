import React, { Component } from 'react';
import { hot } from 'react-hot-loader';
import {BrowserRouter as Router, Route } from 'react-router-dom';
import Sorular from './views/Sorular';
import EgitimSerisi from './views/EgitimSerisi';
import KullaniciYonetimi from './views/KullaniciYonetimi';
import Egitmenler from './views/Egitmenler';
import Katilimcilar from './views/Katilimcilar';
import SirketBilgileri from './views/SirketBilgileri';
import Layout from './components/Layout/LayoutComponent';

class App extends Component {
    render() {
        return (
            <Router>
                <Layout>             
                    <Route exact path="/home" render={() => <h2>Home</h2>}/>
                    <Route path="/home/sorular" component={Sorular} />
                    <Route path="/home/egitimserisi" component={EgitimSerisi} /> 
                    <Route path="/home/kullaniciyonetimi" component={KullaniciYonetimi} /> 
                    <Route path="/home/egitmenler" component={Egitmenler} /> 
                    <Route path="/home/katilimcilar" component={Katilimcilar} /> 
                    <Route path="/home/sirketbilgileri" component={SirketBilgileri} />
                </Layout>
            </Router>
        );
    }
}

export default hot(module)(App);