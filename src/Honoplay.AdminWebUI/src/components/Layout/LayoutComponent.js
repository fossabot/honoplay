import React from 'react';
import { QUESTIONS, TRAINER_SERIES, TRAINERS, 
         TRAINEES, TENANT_INFORMATION, USER_MANAGEMENT, 
         REPORTS } from '../../helpers/TerasuKey';
import MenuIcon from '@material-ui/icons/Menu';
import { withStyles } from '@material-ui/core/styles';
import {AppBar, CssBaseline, Drawer, 
        Hidden, IconButton,Toolbar, 
        Button,List} from '@material-ui/core';
import { Style } from './Style';

import TrainerCard from './TrainerCard';
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
  handleDrawerToggle()
  {
    this.setState(state => ({ mobileOpen: !state.mobileOpen }));
  };

  render() {
    const { classes, theme , children} = this.props;
    const drawer = (
        <div className={classes.drawerLayout}> 
          <div className={classes.companyLayout}>
            <CompanyCard companyName="Framer Bilişim Teknolojileri"/>
          </div>      
          <div className={classes.listLayout}>
            <List 
            component="nav">
            <ListItem 
              pageLink={"/home/sorular"} 
              pageIcon={"question-circle"} 
              pageName={QUESTIONS} />
            <ListItem
              pageLink={"/home/egitimserisi"} 
              pageIcon={"list-ol"} 
              pageName={TRAINER_SERIES}  />
            <ListItem 
              pageLink={"/home/egitmenler"} 
              pageIcon={"graduation-cap"} 
              pageName={TRAINERS} />
            <ListItem 
              pageLink={"/home/katilimcilar"} 
              pageIcon={"users"} 
              pageName={TRAINEES} />
            <ListItem
              pageLink={"/home/sirketbilgileri"}
              pageIcon={"briefcase"}
              pageName={TENANT_INFORMATION} />
            <ListItem 
              pageLink={"/home/kullaniciyonetimi"} 
              pageIcon={"cog"} 
              pageName={USER_MANAGEMENT} />
            <ListItem
              pageLink={"/home/raporlar"} 
              pageIcon={"chart-pie"} 
              pageName={REPORTS} />
              </List>     
          </div>        
          <div  className={classes.trainerLayout}>
            <TrainerCard trainerName="Ahmet Frankfurt"/>
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