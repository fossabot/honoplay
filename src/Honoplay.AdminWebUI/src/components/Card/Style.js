export const Style = (theme) => ({
  card: {
    paddingBottom: 37,
    maxWidth: '90%',
    margin: theme.spacing.unit / 1,
  },
  cardLabel: {
    color: '#707070',
    textAlign: 'center',
    fontSize: 15,
    paddingBottom: 15
  },
  cardDate: {
    color: '#8d8d8d',
    textAlign: 'center',
    fontSize: 12,
    paddingBottom: 15
  },
  center: {
    display: 'flex',
    justifyContent: 'center'
  },
  cardIcon: {
    display: 'flex',
    justifyContent: 'flex-end'
  },
  chip: {
    margin: theme.spacing.unit / 2,
  },
  cardRoot: {
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