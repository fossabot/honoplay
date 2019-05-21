import {deepPurple, deepOrange} from '@material-ui/core/colors';
const drawerWidth = 240;
export const Style = (theme) => ({
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
        display: 'none'
      },
    },
    menuButton: {
      marginRight: 20,
      [theme.breakpoints.up('sm')]: {
        display: 'none',
      },
    },
    drawerPaper: {
      width: drawerWidth,
      background: '#fafafa',
      boxShadow: '0 .5rem 1rem rgba(0,0,0,.25)',
      borderRight: 'none',  
    },
    content: {
      flexGrow: 1,
      padding: theme.spacing.unit * 3,
      paddingTop: 75,
    },
    toolbar: {
      background: '#ff8a65',
    },
    drawerLayout: {
      display: 'flex',
      flex: 1,
      flexDirection:'column',
    },
    listLayout: {
      flex:10,
    },
    trainerLayout: {
      flex:1,
      justifyContent: 'flex-end',
    },
    companyLayout: {
      flex:3,
    },
    listItemLink: {
      padding: 10,        
      paddingLeft: 25
    },
    fontawesome:{
      color: deepOrange[300],
      fontSize: 17,  
    },
    typography: {
      color: deepOrange[300],
      fontSize: 14, 
    },  
    active: {
       background: 'rgba(255, 140, 0, 0.12)',
       borderLeftColor: '#ff8a65',
       borderLeftStyle: 'solid',
       borderLeftWidth: 4
    },
    activeFontawesome: {
      color: '#e92428',
    },
    companyCard: {
      width: 240,
    },
    cardMedia: {
      width: 48,
      borderRadius:4
    },
    trainerCard: {
      width: drawerWidth,
    },
    nested: {
      paddingLeft: theme.spacing.unit * 4,
    },
});

import {createMuiTheme} from '@material-ui/core';
export const theme = createMuiTheme({
  palette: {
    primary: deepPurple,
    secondary: {
      main: deepOrange[300]
    }
  },
  typography: {
    useNextVariants: true,
  },
});
