import React from 'react';
import MaskedInput from 'react-text-mask';
import NumberFormat from 'react-number-format';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import {InputBase, Grid, InputLabel} from '@material-ui/core';
import Style from './Style';

function TextMaskCustom(props) {
  const { inputRef, ...other } = props;

  return (
    <MaskedInput
      {...other}
      ref={ref => {
        inputRef(ref ? ref.inputElement : null);
      }}
      mask={['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/,'-', /\d/, /\d/]}
      placeholderChar={'\u2000'}
      showMask
    />
  );
}

TextMaskCustom.propTypes = {
  inputRef: PropTypes.func.isRequired,
};

function NumberFormatCustom(props) {
  const { inputRef, onChange, ...other } = props;

  return (
    <NumberFormat
      {...other}
      getInputRef={inputRef}
      onValueChange={values => {
        onChange({
          target: {
            value: values.value,
          },
        });
      }}
      thousandSeparator
      prefix="$"
    />
  );
}

NumberFormatCustom.propTypes = {
  inputRef: PropTypes.func.isRequired,
  onChange: PropTypes.func.isRequired,
};

class NumberInputComponent extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
        textmask: '(5  )    -    ',
        
      };
      this.handleChange=this.handleChange.bind(this);
  }


  handleChange  (name, event)  {
    this.setState({
      [name]: event.target.value,
    });
  };

  render() {
    const { classes, LabelName, InputId } = this.props;
    const { textmask } = this.state;

    return (
      <div className={classes.root}>
        <Grid container spacing={24}>
          <Grid item xs={3} className={classes.center}>
            <InputLabel  htmlFor="bootstrap-input" 
                         className={classes.bootstrapFormLabel}>
                         {LabelName}
            </InputLabel>                                   
          </Grid>
          <Grid item xs={9}>
            <InputBase
                value={textmask}
                onChange={this.handleChange.bind(this,('textmask'))}
                id={InputId}
                inputComponent={TextMaskCustom}
                fullWidth
                classes={{
                root: classes.bootstrapRoot,
                input: classes.bootstrapInput,
                }}
            />       
          </Grid>
        </Grid>
    </div>
    );
  }
}

NumberInputComponent.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(Style)(NumberInputComponent);
