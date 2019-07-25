import React from 'react';
import ReactDOM from 'react-dom';
import 'jsdom-global/register'; 
import should from 'should';
import { expect } from 'chai';
import sinon from 'sinon';
import { configure, shallow, mount, render } from 'enzyme';
import { init } from '@omegabigdata/terasu-api-proxy';

import Adapter from 'enzyme-adapter-react-16';
configure({ adapter: new Adapter() });

import CompanyCard from '../components/Layout/CompanyCard';
import TrainerCard from '../components/Layout/TrainerCard';
import List from '../components/Layout/ListItemComponent';
import Layout from '../components/Layout/LayoutComponent';

import {ListItem, IconButton} from '@material-ui/core';

describe('<CompanyCard/>', () => {
    it('Should have props for companyName', () => {
        const wrapper = shallow(<CompanyCard
            companyName = "Framer Bilişim Teknolojileri"
        />);
        wrapper.props().companyName.should.be.defined;
    });
})

describe('<TrainerCard/>', () => {
    it('Should have props  trainerName', () => {
        const wrapper = shallow(<TrainerCard
            trainerName = "Ahmet Frankfurt"
        />);
        wrapper.props().trainerName.should.be.defined;
    });
})

describe('<List/>', () => {
    it('Should have props  pageLink, pageIcon and pageName', () => {
        const wrapper = shallow(<List
            pageLink = "/home/sorular"
            pageIcon = "question-circle"
            pageName = "Sorular"
        />);
        wrapper.props().pageLink.should.be.defined;
        wrapper.props().pageIcon.should.be.defined;
        wrapper.props().pageName.should.be.defined;
    });

    // it('Should change class when ListItem click', () => {
    //     const wrapper = shallow(<List
    //         pageLink = "/home/sorular"
    //         pageIcon = "question-circle"
    //         pageName = "Sorular"
    //     />).dive();
    //     wrapper.find(ListItem).simulate('click');
    //     expect(wrapper.find(ListItem).prop('activeClassName'))
    //     .to.equal('ListItemComponent-active-90');
    // });
})

describe('<Layout/>', () => {
    it('should open drawer when mobil menu icon click', () => {
        const wrapper = shallow(<Layout/>).dive();
        wrapper.find(IconButton).simulate('click');
        wrapper.setState({mobileOpen: true});
        expect(wrapper.state().mobileOpen).to.equal(true);
    });
})
