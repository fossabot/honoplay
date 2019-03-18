import React from 'react';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faQuestionCircle, faListOl, faGraduationCap, faUsers, faCog, faChartPie } from '@fortawesome/free-solid-svg-icons';
library.add(faQuestionCircle, faListOl, faGraduationCap, faUsers, faCog, faChartPie );

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import PropTypes from 'prop-types';
import MenuIcon from '@material-ui/icons/Menu';

import { withStyles } from '@material-ui/core/styles';

import {AppBar, CssBaseline, Drawer, 
        Hidden, IconButton,List, 
        ListItem, ListItemText,Toolbar,
        Typography, Divider} from '@material-ui/core';

const drawerWidth = 240;
const styles = theme => ({
  root: {
    display: 'flex',
  },
  drawer: {
    [theme.breakpoints.up('sm')]: {
      width: drawerWidth,
      flexShrink: 0,
    },
  },
  appBar: {
    marginLeft: drawerWidth,
    [theme.breakpoints.up('sm')]: {
      display:'none',
    },
  },
  menuButton: {
    marginRight: 20,
    [theme.breakpoints.up('sm')]: {
      display: 'none',
    },
  },
  toolbar: theme.mixins.toolbar, //yukardan boşluk veriyor
  drawerPaper: {
    width: drawerWidth,
    background: '#e92428',
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing.unit * 3,
  },
  Divider: {
    height:1,
    backgroundColor:'#e48e8f'
  },
  List: {
    paddingTop:0,
  },
  ListItemLink: {
    paddingTop:35,
    paddingBottom:35
  },
  fontawesome: {
    color: 'white',
    fontSize: 17
  },
  Typography: {
    color: 'white',
    fontSize: 17
 }
});

function ListItemLink(props) {
  return <ListItem button component="a" {...props} />;
}

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
    const { classes, theme } = this.props;

    const drawer = (
      <div>
        <div className={classes.toolbar} />
        <Divider className={classes.Divider} />
        <List component="nav" className={classes.List}>
            <ListItemLink className={classes.ListItemLink} href="#simple-list">
              <FontAwesomeIcon className={classes.fontawesome} icon="question-circle" />
              <ListItemText
                primary={<Typography className={classes.Typography}> Sorular </Typography>}
              />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
            <ListItemLink className={classes.ListItemLink} href="#simple-list">
              <FontAwesomeIcon className={classes.fontawesome} icon="list-ol" />
              <ListItemText
                primary={<Typography className={classes.Typography}> Eğitim Serisi </Typography>}
              />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
            <ListItemLink className={classes.ListItemLink} href="#simple-list">
              <FontAwesomeIcon className={classes.fontawesome} icon="graduation-cap" />
              <ListItemText
                primary={<Typography className={classes.Typography}> Eğitmenler </Typography>}
              />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
            <ListItemLink className={classes.ListItemLink} href="#simple-list">
              <FontAwesomeIcon className={classes.fontawesome} icon="users" />
              <ListItemText
                primary={<Typography className={classes.Typography}> Katılımcılar </Typography>}
              />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
            <ListItemLink className={classes.ListItemLink} href="#simple-list">
              <FontAwesomeIcon className={classes.fontawesome} icon="cog" />
              <ListItemText
                primary={<Typography className={classes.Typography}> Kullanıcı Yönetimi </Typography>}
              />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
            <ListItemLink className={classes.ListItemLink} href="#simple-list">
              <FontAwesomeIcon className={classes.fontawesome} icon="chart-pie" />
              <ListItemText
                primary={<Typography className={classes.Typography}> Raporlar </Typography>}
              />             
            </ListItemLink>
            <Divider className={classes.Divider}/>
        </List>
      </div>
    );

    return (
      <div className={classes.root}>
        <CssBaseline />
        <AppBar position="fixed" className={classes.appBar}>
          <Toolbar style={{background: '#e92428'}}>
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
          <Typography paragraph>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
          </Typography>
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

export default withStyles(styles, { withTheme: true })(Layout);