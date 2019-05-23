import React from 'react';
import terasuProxy from '@omegabigdata/terasu-api-proxy';
import { tenantInformation, save, basicKnowledge, 
         department, trainees } from '../../helpers/TerasuKey';
import { withStyles } from '@material-ui/core/styles';
import SwipeableViews from 'react-swipeable-views';
import {Tabs, Tab, MuiThemeProvider, Grid} from '@material-ui/core';
import TabContainer from './TabContainer';
import Typography from '../../components/Typography/TypographyComponent';
import Button from '../../components/Button/ButtonComponent';
import {Style, theme} from './Style';

import TemelBilgiler from '../../views/SirketBilgileri/TemelBilgiler';
import Departman from '../../views/SirketBilgileri/Departman';
import Kisiler from '../../views/SirketBilgileri/Kisiler';

class FullWidthTabs extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
          value: 0,
      };
      this.handleChange=this.handleChange.bind(this);
      this.handleChangeIndex=this.handleChangeIndex.bind(this);
      this.handleChangeValue=this.handleChangeValue.bind(this);
  }

  handleChange(event, value)
  {
    this.setState({ value });
  };

  handleChangeIndex(index)
  {
    this.setState({ value: index });
  };
  handleChangeValue(event)
  {
    this.setState({ 
      value: this.state.value === 2 ? this.state.value : this.state.value + 1
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
              pageHeader={terasuProxy.translate(tenantInformation)}
            />
          </Grid>
          <Grid item xs={6} sm={2}>
            <Button 
              buttonColor="secondary"                    
              buttonName={terasuProxy.translate(save)}                   
              onClick={this.handleChangeValue}
            />
          </Grid>
          <Grid item xs={12} sm={12}>
            <Tabs
              indicatorColor="secondary"
              value={this.state.value}
              onChange={this.handleChange}
              textColor="primary"
              variant="fullWidth">
                <Tab 
                  className={ this.state.value === 0 ? 
                  classes.active_tab : classes.default_tabStyle } 
                    label={terasuProxy.translate(basicKnowledge)}
                />
                <Tab 
                  className={ this.state.value === 1 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={terasuProxy.translate(department)}
                  disabled={this.state.value === 0 ? true : false}
                />
                <Tab 
                  className={ this.state.value === 2 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={terasuProxy.translate(trainees)}
                  disabled={this.state.value === 0 || this.state.value === 1 ? true : false}
                />
            </Tabs>
            <SwipeableViews
              axis={theme.direction === 'rtl' ? 'x-reverse' : 'x'}
              index={this.state.value}
              onChangeIndex={this.handleChangeIndex}
            >
              <TabContainer dir={theme.direction}>
                <TemelBilgiler/>
              </TabContainer>
              <TabContainer dir={theme.direction}>
                <Departman/>
              </TabContainer>
              <TabContainer dir={theme.direction}>
                <Kisiler/>
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