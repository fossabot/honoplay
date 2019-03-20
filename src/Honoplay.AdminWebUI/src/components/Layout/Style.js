const drawerWidth = 240;
const Style = (theme) => ({
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
    toolbar: theme.mixins.toolbar,
    drawerPaper: {
      width: drawerWidth,
      background: '#e92428',
      boxShadow: '0 .5rem 1rem rgba(0,0,0,.15)',
      borderRight: 'none',  
    },
    content: {
      flexGrow: 1,
      padding: theme.spacing.unit * 3,
      paddingTop: 0
    },
    Divider: {
      height:1,
      backgroundColor:'#e48e8f'
    },
    List: {
      paddingTop:0,
    },
    Toolbar: {
      background: '#e92428',
    },
    ListItemLink: {
      paddingTop:20,
      paddingBottom:20
    },
    fontawesome:{
      color: 'white',
      fontSize: 15,      
    },
    Typography: {
      color: 'white',
      fontSize: 15
    },  
    active: {
       background: 'rgba(255, 255, 255, 0.12)',
    },
    activeFontawesome: {
      color: '#e92428',
      fontSize: 15,
    }
});

export default Style;