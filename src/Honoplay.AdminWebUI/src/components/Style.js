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
    fontawesome: {
        color: 'white',
        fontSize: 15
    },
    Typography: {
        color: 'white',
        fontSize: 15
    },  
});

export default Style;