import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import SwipeableViews from 'react-swipeable-views';
import {Tabs, Tab, MuiThemeProvider} from '@material-ui/core';
import TabContainer from './TabContainer';
import {Style, theme} from './Style';

class FullWidthTabs extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
          value: 0,
      };
      this.handleChange=this.handleChange.bind(this);
      this.handleChangeIndex=this.handleChangeIndex.bind(this);
  }

  handleChange(event, value)
  {
    this.setState({ value });
  };

  handleChangeIndex(index)
  {
    this.setState({ value: index });
  };

  render() {
    const { classes } = this.props;
    return (
    <MuiThemeProvider theme={theme}>
      <div className={classes.root}>
        <Tabs
          indicatorColor="secondary"
          value={this.state.value}
          onChange={this.handleChange}
          textColor="primary"
          variant="fullWidth"
        >
          <Tab className={ this.state.value === 0 ? classes.active_tab : classes.default_tabStyle } 
               label="Temel Bilgiler" />
          <Tab className={ this.state.value === 1 ? classes.active_tab : classes.default_tabStyle } 
               label="Departman" />
          <Tab className={ this.state.value === 2 ? classes.active_tab : classes.default_tabStyle } 
               label="Kişiler" />
        </Tabs>
        <SwipeableViews
          axis={theme.direction === 'rtl' ? 'x-reverse' : 'x'}
          index={this.state.value}
          onChangeIndex={this.handleChangeIndex}
        >
          <TabContainer dir={theme.direction}>Item One</TabContainer>
          <TabContainer dir={theme.direction}>Item Two</TabContainer>
          <TabContainer dir={theme.direction}>Item Three</TabContainer>
        </SwipeableViews>
      </div>
    </MuiThemeProvider>
    );
  }
}

export default withStyles(Style)(FullWidthTabs);