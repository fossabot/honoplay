import React from 'react';
import { translate } from '@omegabigdata/terasu-api-proxy';
import MenuIcon from '@material-ui/icons/Menu';
import { withStyles } from '@material-ui/core/styles';
import {
  AppBar, CssBaseline, Drawer,
  Hidden, IconButton, Toolbar, List
} from '@material-ui/core';
import { Style } from './Style';

import ListItem from './ListItemComponent';
import CompanyCard from './CompanyCard';


class Layout extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      mobileOpen: false,
    };

    this.handleDrawerToggle = this.handleDrawerToggle.bind(this)
  }
  handleDrawerToggle() {
    this.setState(state => ({ mobileOpen: !state.mobileOpen }));
  };

  render() {
    const { classes, theme, children } = this.props;
    const drawer = (
      <div className={classes.drawerLayout}>
        <div className={classes.companyLayout}>
          <CompanyCard/>
        </div>

        <div className={classes.listLayout}>
          <List
            component="nav">
            <ListItem
              pageLink={"/admin/questions"}
              pageIcon={"question-circle"}
              pageName={translate('Questions')} />
            <ListItem
              pageLink={"/admin/trainingseries"}
              pageIcon={"list-ol"}
              pageName={translate('TrainingSeries')} />
            <ListItem
              pageLink={"/admin/trainers"}
              pageIcon={"graduation-cap"}
              pageName={translate('Trainers')} />
            <ListItem
              pageLink={"/admin/tenantinformation"}
              pageIcon={"briefcase"}
              pageName={translate('TenantInformation')} />
            <ListItem
              pageLink={"/admin/usermanagement"}
              pageIcon={"cog"}
              pageName={translate('UserManagement')} />
            <ListItem
              pageLink={"/admin/reports"}
              pageIcon={"chart-pie"}
              pageName={translate('Reports')} />
          </List>
        </div>
      </div>
    );
    return (
      <div className={classes.root}>
        <CssBaseline />
        <AppBar position="fixed" className={classes.appBar}>
          <Toolbar className={classes.toolbar}>
            <IconButton
              color="inherit"
              aria-label="Open drawer"
              onClick={this.handleDrawerToggle.bind(this)}
              className={classes.menuButton}
            >
              <MenuIcon />
            </IconButton>
          </Toolbar>
        </AppBar>
        <nav className={classes.drawer}>
          <Hidden smUp implementation="css">
            <Drawer
              container={this.props.container}
              variant="temporary"
              anchor={theme.direction === 'rtl' ? 'right' : 'left'}
              open={this.state.mobileOpen}
              onClose={this.handleDrawerToggle}
              classes={{
                paper: classes.drawerPaper,
              }}
            >
              {drawer}
            </Drawer>
          </Hidden>
          <Hidden xsDown implementation="css">
            <Drawer
              classes={{
                paper: classes.drawerPaper,
              }}
              variant="permanent"
              open
            >
              {drawer}
            </Drawer>
          </Hidden>
        </nav>
        <main className={classes.content}>
          <div className={classes.toolbar} />
          {children}
        </main>
      </div>
    );
  }
}

export default withStyles(Style, { withTheme: true })(Layout);