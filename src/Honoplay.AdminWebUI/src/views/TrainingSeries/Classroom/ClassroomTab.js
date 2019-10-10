import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { translate } from '@omegabigdata/terasu-api-proxy';
import { Grid, Tabs, Tab, MuiThemeProvider } from '@material-ui/core';
import Style from '../../Style';
import BreadCrumbs from '../../../components/BreadCrumbs/BreadCrumbs';
import SwipeableViews from 'react-swipeable-views';
import { createMuiTheme } from '@material-ui/core';
import { deepPurple, deepOrange } from '@material-ui/core/colors';
import TabContainer from '../../../components/Tabs/TabContainer';
import Classroom from '../Classroom/Classroom';
import Sessions from '../Session/Sessions';

const theme = createMuiTheme({
  palette: {
    primary: deepPurple,
    secondary: {
      main: deepOrange[300]
    }
  },
  typography: {
    useNextVariants: true
  }
});

class TrainingsTab extends React.Component {
  constructor(props) {
    super(props);
    this.state = { tabValue: 1 };
  }

  handleChange = (event, tabValue) => {
    this.setState({ tabValue });
  };

  handleChangeIndex = index => {
    this.setState({ tabValue: index });
  };

  handleClick = () => {
    this.props.history.push(
      `/trainingseries/training/${this.props.match.params.trainingId}/classroom`
    );
  };

  render() {
    const { classes } = this.props;

    return (
      <div className={classes.root}>
        <MuiThemeProvider theme={theme}>
          <Grid container spacing={3}>
            <Grid item xs={12} sm={12}>
              <BreadCrumbs />
            </Grid>
            <Grid item xs={12} sm={12} />
          </Grid>
          <Tabs
            value={this.state.tabValue}
            onChange={this.handleChange}
            indicatorColor="primary"
            textColor="primary"
            className={classes.tabs}
          >
            <Tab label={translate('BasicKnowledge')} className={classes.tab} />
            <Tab label={translate('Sessions')} className={classes.tab} />
          </Tabs>
          <SwipeableViews
            axis={theme.direction === 'rtl' ? 'x-reverse' : 'x'}
            index={this.state.tabValue}
            onChangeIndex={this.handleChangeIndex}
          >
            <TabContainer dir={theme.direction}>
              <Classroom update />
            </TabContainer>
            <TabContainer dir={theme.direction}>
              <Sessions />
            </TabContainer>
          </SwipeableViews>
        </MuiThemeProvider>
      </div>
    );
  }
}

export default withStyles(Style)(TrainingsTab);
