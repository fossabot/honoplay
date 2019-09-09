import { green } from '@material-ui/core/colors';

const Style = theme => ({
  root: {
    flexGrow: 1,
    padding: 8 * 4
  },
  linkStyle: {
    fontSize: 16,
    color: '#673ab7',
    textDecorationLine: 'none',
    '&:active': {
      color: '#673ab7',
    },
  },
  buttonProgress: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 19
  },
  buttonSuccess: {
    backgroundColor: green[500],
    boxShadow: '0 .1rem 1rem rgba(0,128,0.25)',
    '&:hover': {
      backgroundColor: green[700],
    },
  },
  progressStepper: {
    position: 'absolute',
    marginTop: -25,
    marginLeft: 720,
  },
  progressModal: {
    position: 'absolute',
    marginTop: -25,
    marginLeft: 520,
  },
  buttonProgressUpdate: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 32
  },
  buttonProgressSave: {
    color: green[500],
    position: 'absolute',
    marginTop: -30,
    marginLeft: 30
  },
  questionDiv: {
    borderColor: '#9e9e9e',
    borderStyle: 'solid',
    borderWidth: 1,
    height: 70
  },
  passwordInput : {
    width: '94%', 
  },
  bootstrapFormLabel: {
    fontSize: 15,
    color: '#495057'
  },
});

export default Style;
