import { makeStyles } from '@material-ui/core/styles';
export default makeStyles(theme => ({
  root: {
    flexGrow: 1
  },
  div: {
    textAlign: 'left',
    width: '320px',
    height: '100px',
    color: theme.palette.text.secondary
  },
  box: {
    postion: 'absolute',
    left: '60px',
    top: '60px'
  },
  paper: {
    width: '300px',
    height: '90px',
    left: '0',
    padding: 8,
    overflow: 'hidden',
    transitionProperty: 'transform, height, width, left,padding',
    transitionDuration: '0.15s,  0.15s,  0.15s, 0.15s, 0.15s',
    transitionTimingFunction: 'cubic-bezier(.26,.58,0,.9)',
    zIndex: '0',
    position: 'absolute',
    '&:hover': {
      height: '130px',
      paddingLeft: 16,
      paddingTop: 8,
      paddingRight: 16,
      paddingBottom: 8,
      width: '316px',
      left: '-8px',
      zIndex: '1'
    }
  },
  divRoot: {
    display: 'flex',
    flexWrap: 'wrap'
  },
  div1: {
    display: 'flex',
    flexWrap: 'wrap'
  },
  div2: {
    position: 'relative'
  },
  div3: {
    position: 'relative'
  },
  text: {
    left: 0,
    textTransform: 'uppercase'
  },
  div4: {
    position: 'absolute',
    right: 0,
    top: 0
  }
}));
