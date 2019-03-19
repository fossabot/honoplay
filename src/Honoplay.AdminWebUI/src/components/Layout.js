import React from 'react';
import PropTypes from 'prop-types';
import MenuIcon from '@material-ui/icons/Menu';
import { withStyles } from '@material-ui/core/styles';
import MenuItem from './MenuItem';
import {AppBar, CssBaseline, Drawer, 
        Hidden, IconButton,Toolbar, Divider,List} from '@material-ui/core';
import Style from './Style';

class Layout extends React.Component {

  constructor(props) {
    super(props);
        
    this.state = {
        mobileOpen: false,
    };

    this.handleDrawerToggle = this.handleDrawerToggle.bind(this)
  }
  handleDrawerToggle()
  {
    this.setState(state => ({ mobileOpen: !state.mobileOpen }));
  };

  render() {
    const { classes, theme , children} = this.props;
    const drawer = (
      <div>
        <div className={classes.toolbar} />
        <Divider className={classes.Divider} />
        <List component="nav" className={classes.List}>
          <MenuItem pageLink={"/sorular"} pageIcon={"question-circle"} pageName={"Sorular"} />
          <MenuItem pageLink={"/egitimserisi"} pageIcon={"list-ol"} pageName={"Eğitim Serisi"} />
          <MenuItem pageLink={"/egitmenler"} pageIcon={"graduation-cap"} pageName={"Eğitmenler"} />
          <MenuItem pageLink={"/katilimcilar"} pageIcon={"users"} pageName={"Katılımcılar"} />
          <MenuItem pageLink={"/raporlar"} pageIcon={"chart-pie"} pageName={"Raporlar"} />
        </List>
      </div>
    );
    return (
      <div className={classes.root}>
        <CssBaseline />
        <AppBar position="fixed" className={classes.appBar}>
          <Toolbar className={classes.Toolbar}>
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

Layout.propTypes = {
  classes: PropTypes.object.isRequired,
  container: PropTypes.object,
  theme: PropTypes.object.isRequired,
};

export default withStyles(Style, { withTheme: true })(Layout);