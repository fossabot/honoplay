import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import SwipeableViews from 'react-swipeable-views';
import {Tabs, Tab, MuiThemeProvider, Grid} from '@material-ui/core';
import TabContainer from './TabContainer';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import {Style, theme} from './Style';

import BasicKnowledge from '../../views/TenantInformation/BasicKnowledge';
import Department from '../../views/TenantInformation/Department';
import Trainee from '../../views/TenantInformation/Trainee';

class FullWidthTabs extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
          tabValue: 0,
      };
      this.handleChange=this.handleChange.bind(this);
      this.handleChangeIndex=this.handleChangeIndex.bind(this);
      this.handleChangetabValue=this.handleChangetabValue.bind(this);
  }

  handleChange(event, tabValue)
  {
    this.setState({ tabValue });
  };

  handleChangeIndex(index)
  {
    this.setState({ tabValue: index });
  };
  handleChangetabValue(event)
  {
    this.setState({ 
      tabValue: this.state.tabValue === 2 ? this.state.tabValue : this.state.tabValue + 1
    });
  };

  render() {
    const { classes } = this.props;
    return (
    <MuiThemeProvider theme={theme}>
      <div className={classes.root}>
      <Grid container spacing={24}>
        <Grid item xs={6} sm={10}>
            <Typography 
              pageHeader={translate('TenantInformation')}
            />
          </Grid>
          <Grid item xs={6} sm={2}>
            <Button 
              buttonColor="secondary"                    
              buttonName={translate('Save')}                   
              onClick={this.handleChangetabValue}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Tabs
              indicatorColor="secondary"
              value={this.state.tabValue}
              onChange={this.handleChange}
              textColor="primary"
              variant="fullWidth">
                <Tab 
                  className={ this.state.tabValue === 0 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={translate('BasicKnowledge')}
                />
                <Tab 
                  className={ this.state.tabValue === 1 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={translate('Department')}
                  disabled={this.state.tabValue === 0 ? true : false}
                />
                <Tab 
                  className={ this.state.tabValue === 2 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={translate('Trainees')}
                  disabled={this.state.tabValue === 0 || this.state.tabValue === 1 ? true : false}
                />
            </Tabs>
            <SwipeableViews
              axis={theme.direction === 'rtl' ? 'x-reverse' : 'x'}
              index={this.state.tabValue}
              onChangeIndex={this.handleChangeIndex}
            >
              <TabContainer dir={theme.direction}>
                <BasicKnowledge/>
              </TabContainer>
              <TabContainer dir={theme.direction}>
                <Department/>
              </TabContainer>
              <TabContainer dir={theme.direction}>
                <Trainee/>
              </TabContainer>
            </SwipeableViews>
          </Grid>
      </Grid>
      </div>
    </MuiThemeProvider>
    );
  }
}

export default withStyles(Style)(FullWidthTabs);