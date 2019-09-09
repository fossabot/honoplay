import { createMuiTheme } from '@material-ui/core';

export const Style = (theme) => ({
  card: {
    maxWidth: '90%',
    margin: theme.spacing(1),
  },
  cardLabel: {
    color: '#707070',
    textAlign: 'center',
    paddingBottom: 15
  },
  cardDate: {
    color: '#8d8d8d',
    textAlign: 'center',
    paddingBottom: 15
  },
  center: {
    display: 'flex',
    justifyContent: 'center',
    paddingBottom: 10
  },
  cardIcon: {
    display: 'flex',
    justifyContent: 'flex-end'
  },
  chip: {
    margin: theme.spacing(0.5),
  },
  cardRoot: {
    display: 'flex',
    justifyContent: 'left',
    flexWrap: 'wrap',
    padding: theme.spacing(0.5),
  },
  cardButton: {
    maxWidth: '60%',
    backgroundColor: '#444444',
  },
  cardButtonTraining: {
    maxWidth: '60%',
    backgroundColor: '#673ab7',
  },
  iconCardButton: {
    color: 'white',
    fontSize: 40,
  },
  iconCard: {
    color: 'black',
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
    margin: theme.spacing(1),
    fontWeight: 'bold',
    fontSize: 15
  },
  paragraphCardButton: {
    color: 'white',
    margin: theme.spacing(1),
    textAlign: 'center',
    width: '100%',
    fontSize: 13
  },
  infoCard: {
    maxWidth: '%100',
  },

});

export const theme = createMuiTheme({
  typography: {
    useNextVariants: true,
  },
});