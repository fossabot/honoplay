export const Style = (theme) => ({
  root: {
    display: 'flex',
    justifyContent: 'left',
    flexWrap: 'wrap',
    padding: theme.spacing.unit / 2,
  },
  cardButton: {
    maxWidth: '60%',
    backgroundColor: '#444444',
  },
  cardButtonTraining: {
    maxWidth: '60%',
    backgroundColor: '#ff8a65',
  },
  iconCardButton: {
    color: 'white',
    fontSize: 40,
  },
  centerCardButton: {
    display: 'flex',
    justifyContent: 'center',
    paddingTop: 15,
  },
  typographyCardButton: {
    color: 'white',
    textAlign: 'center',
    margin: theme.spacing.unit,
    fontWeight: 'bold',
    fontSize: 15
  },
  paragraphCardButton: {
    color: 'white',
    margin: theme.spacing.unit,
    textAlign: 'center',
    width: '100%',
    fontSize: 13
  },

});

import {createMuiTheme} from '@material-ui/core';
export const theme = createMuiTheme({
  typography: {
    useNextVariants: true,
  },
});