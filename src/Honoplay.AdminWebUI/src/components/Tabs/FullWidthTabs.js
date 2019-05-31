import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { withStyles } from '@material-ui/core/styles';
import classNames from 'classnames';
import SwipeableViews from 'react-swipeable-views';
import {Tabs, Tab, MuiThemeProvider, Grid, 
        CircularProgress, Button} from '@material-ui/core';
import { SnackbarProvider, withSnackbar } from 'notistack';        
import {Style, theme} from './Style';
import TabContainer from './TabContainer';
import Typography from '../../components/Typography/TypographyComponent';

import BasicKnowledge from '../../views/TenantInformation/BasicKnowledge';
import Department from '../../views/TenantInformation/Department';
import Trainee from '../../views/TenantInformation/Trainee';


class FullWidthTabs extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
          tabValue: 0,
          loading: false,
          success: false,
          disabled: true,
          disabled2: true,
      };
      this.handleChange=this.handleChange.bind(this);
      this.handleChangeIndex=this.handleChangeIndex.bind(this);
      this.handleButtonClick=this.handleButtonClick.bind(this,('success'));
  }

  componentWillUnmount() {
    clearTimeout(this.timer);
  }

  handleButtonClick (variant)
  {
   if (!this.state.loading) {
     this.setState(
       {
         success: false,
         loading: true,
       },
       () => {
         this.timer = setTimeout(() => {
           this.setState({
             loading: false,
             success: true,
 
           });   
 
         }, 2000);
         this.timer = setTimeout(() => {
          this.setState({
            success: false,  
            disabled: false,       
          }); 
          {this.state.tabValue === 1 && this.state.success === false && 
            this.setState({disabled2 : false})
          } 
          this.props.enqueueSnackbar('İşleminiz başarılı bir şekilde kaydedildi!', { variant });
        }, 3000);
       },
     );
   }
  };

  handleChange(event, tabValue)
  {
    this.setState({ tabValue });
  };

  handleChangeIndex(index)
  {
    this.setState({ tabValue: index });
  };

  render() {
    const { loading, success, tabValue, disabled } = this.state;
    const { classes } = this.props;
    const buttonClassname = classNames({
      [classes.buttonSuccess]: success,
    });

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
          <div className={classes.buttonRoot}>
            <div className={classes.buttonWrapper}>
              <Button
                variant="contained"
                color="primary"
                className={buttonClassname}
                disabled={loading}
                onClick={this.handleButtonClick}
              >
                {translate('Save')}
              </Button>
              {loading && <CircularProgress size={24} className={classes.buttonProgress}/>}
            </div>
          </div>
          </Grid>
          <Grid item xs={12} sm={12}>
            <Tabs
              indicatorColor="secondary"
              value={tabValue}
              onChange={this.handleChange}
              textColor="primary"
              variant="fullWidth">
                <Tab 
                  className={ tabValue === 0 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={translate('BasicKnowledge')}
                />
                <Tab 
                  className={ tabValue === 1 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={translate('Department')}
                  disabled={this.state.disabled }
                />
                <Tab 
                  className={ tabValue === 2 ? 
                  classes.active_tab : classes.default_tabStyle } 
                  label={translate('Trainees')}
                  disabled={ this.state.disabled2 }
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

const MyApp = withSnackbar(withStyles(Style)(FullWidthTabs));

function IntegrationNotistack() {
  return (
    <SnackbarProvider maxSnack={3}>
      <MyApp />
    </SnackbarProvider>
  );
}

export default IntegrationNotistack;