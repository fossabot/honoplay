export const Style = (theme) => ({
  headRoot: {
    paddingRight: theme.spacing(1),
  },
  headSpacer: {
    flex: '1 1 100%',
  },
  headTitle: {
    flex: '0 0 auto',
  },
  tableMenu: {
    fontSize:'18px'
  },
  tableRoot: {
    width: '100%',
    marginTop: theme.spacing(1) * 3,
  },
  table: {
    minWidth: 90,
  },
  tableWrapper: {
    overflowX: 'auto',
  },
  tableCell: {
    // paddingRight: 2,
    // paddingLeft: 2,
  },
  simpleTableRoot: {
    width: '100%',
    marginTop: theme.spacing(1) * 3,
    overflowX: 'auto',
    maxHeight: 350, 
  },
  simpleTable: {  
    minWidth: 350,
  },
  tablecellText: {
    color:'#673ab7',
    fontSize: 15
  },
  text: {
    color: '#757575'
  }
});

import {createMuiTheme} from '@material-ui/core';
import {deepPurple, deepOrange} from '@material-ui/core/colors';
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
